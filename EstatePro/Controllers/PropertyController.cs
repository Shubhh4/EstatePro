using EstatePro.Data;
using EstatePro.Models;
using EstatePro.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EstatePro.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyRepository _repo;
        private readonly ApplicationDbContext _context;

        public PropertyController(IPropertyRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            int userId = (int)HttpContext.Session.GetInt32("UserId");
            string role = HttpContext.Session.GetString("UserRole");

            var properties = _repo.GetAll();

            if (role == "Agent" || role == "Seller")
            {
                properties = properties.Where(p => p.OwnerId == userId && p.OwnerRole.ToString() == role);
            }

            return View(properties);
        }

        public IActionResult Details(int id)
        {
            var property = _repo.GetById(id);
            if (property == null) return NotFound();
            return View(property);
        }

        public IActionResult Create()
        {
            ViewBag.PropertyTypes = new SelectList(Enum.GetValues(typeof(PropertyType)));
            ViewBag.ListingTypes = new SelectList(Enum.GetValues(typeof(ListingType)));
            ViewBag.Statuses = new SelectList(Enum.GetValues(typeof(PropertyStatus)));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Property property, List<IFormFile> images)
        {
            if (property.CreatedAt == null)
                property.CreatedAt = DateTime.UtcNow;

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("Validation error: " + error.ErrorMessage);
                }

                //ViewBag.Owners = new SelectList(_context.Users.ToList(), "UserId", "Email");
                return View(property);
            }

            try
            {
                property.OwnerId = (int)HttpContext.Session.GetInt32("UserId");
                property.OwnerRole = (OwnerRole)Enum.Parse(typeof(OwnerRole), HttpContext.Session.GetString("UserRole"));
                _repo.Add(property, images);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return View(property);
            }
        }


        public IActionResult Edit(int id)
        {
            var property = _repo.GetById(id);
            if (property == null) return NotFound();

            int userId = (int)HttpContext.Session.GetInt32("UserId");
            string role = HttpContext.Session.GetString("UserRole");

            // Prevent unauthorized edit
            if ((role == "Agent" || role == "Seller") && property.OwnerId != userId)
                return Forbid();

            ViewBag.PropertyTypes = new SelectList(Enum.GetValues(typeof(PropertyType)));
            ViewBag.ListingTypes = new SelectList(Enum.GetValues(typeof(ListingType)));
            ViewBag.Statuses = new SelectList(Enum.GetValues(typeof(PropertyStatus)));

            return View(property);
        }


        [HttpPost]
        public IActionResult Edit(Property property, List<IFormFile> images)
        {
            int userId = (int)HttpContext.Session.GetInt32("UserId");
            string role = HttpContext.Session.GetString("UserRole");

            var existingProperty = _repo.GetById(property.PropertyId);
            if (existingProperty == null) return NotFound();

            if ((role == "Agent" || role == "Seller") && existingProperty.OwnerId != userId)
                return Forbid();

            if (ModelState.IsValid)
            {
                property.OwnerId = userId;
                property.OwnerRole = (OwnerRole)Enum.Parse(typeof(OwnerRole), role);
                _repo.Update(property, images);
                return RedirectToAction("Index");
            }

            ViewBag.PropertyTypes = new SelectList(Enum.GetValues(typeof(PropertyType)));
            ViewBag.ListingTypes = new SelectList(Enum.GetValues(typeof(ListingType)));
            ViewBag.Statuses = new SelectList(Enum.GetValues(typeof(PropertyStatus)));

            return View(property);
        }


        public IActionResult Delete(int id)
        {
            var property = _repo.GetById(id);
            if (property == null) return NotFound();

            int userId = (int)HttpContext.Session.GetInt32("UserId");
            string role = HttpContext.Session.GetString("UserRole");

            if ((role == "Agent" || role == "Seller") && property.OwnerId != userId)
                return Forbid();

            return View(property);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var property = _repo.GetById(id);
            if (property == null) return NotFound();

            int userId = (int)HttpContext.Session.GetInt32("UserId");
            string role = HttpContext.Session.GetString("UserRole");

            if ((role == "Agent" || role == "Seller") && property.OwnerId != userId)
                return Forbid();

            _repo.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Browse()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var role = HttpContext.Session.GetString("UserRole");

            var properties = _repo.GetAll();

            // Filter based on role
            if (role == "Buyer")
                properties = properties.Where(p => p.ListingType == ListingType.Sale && p.Status==PropertyStatus.Available);
            else if (role == "Tenant")
                properties = properties.Where(p => p.ListingType == ListingType.Rent && p.Status == PropertyStatus.Available);

            return View(properties);
        }

    }
}
