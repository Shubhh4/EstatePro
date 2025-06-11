using EstatePro.Data;
using EstatePro.Models;
using EstatePro.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EstatePro.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly AppointmentService _service;
        private readonly ApplicationDbContext _context;

        public AppointmentController(AppointmentService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString("UserRole");
            var userId = HttpContext.Session.GetInt32("UserId");

            var appointments = await _service.GetAllAsync();

            if (role == "Agent" || role == "Seller")
            {
                appointments = appointments
                    .Where(a => a.Property.OwnerId == userId) // Make sure Property has OwnerId
                    .ToList();
            }
            else if (role == "Buyer" || role == "Tenant")
            {
                appointments = appointments
                    .Where(a => a.UserId == userId)
                    .ToList();
            }

            return View(appointments);
        }


        public IActionResult Create(int propertyId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            var property = _context.Properties.FirstOrDefault(p => p.PropertyId == propertyId);
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (property == null || user == null) return NotFound();

            var appointment = new Appointment
            {
                PropertyId = propertyId,
                UserId = userId.Value,
                AppointmentDate = DateTime.Now.AddDays(1),
                Status = AppointmentStatus.Pending
            };

            ViewBag.PropertyTitle = property.Title;
            ViewBag.UserEmail = user.Email;

            return View(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return Unauthorized();

            // Set required fields BEFORE ModelState check
            appointment.UserId = userId.Value;
            appointment.Status = AppointmentStatus.Pending;
            await _service.AddAsync(appointment);
            return RedirectToAction("Index", "Appointment");
            // PropertyId should already be posted from hidden field
            if (!ModelState.IsValid)
            {
                var property = _context.Properties.FirstOrDefault(p => p.PropertyId == appointment.PropertyId);
                var user = _context.Users.FirstOrDefault(u => u.UserId == appointment.UserId);

                ViewBag.PropertyTitle = property?.Title;
                ViewBag.UserEmail = user?.Email;

                return View(appointment);
            }
        }



        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _service.GetByIdAsync(id);
            if (appointment == null) return NotFound();

            ViewBag.Properties = new SelectList(_context.Properties, "PropertyId", "Title", appointment.PropertyId);
            ViewBag.Users = new SelectList(_context.Users, "UserId", "Email", appointment.UserId);
            return View(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(appointment);
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _service.GetByIdAsync(id);
            return View(appointment);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var appointment = await _service.GetByIdAsync(id);
            return View(appointment);
        }

        public async Task<IActionResult> UpdateStatus(int id, AppointmentStatus status)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin" && role != "Agent" && role != "Seller")
            {
                return Unauthorized(); // or RedirectToAction("AccessDenied")
            }

            var appointment = await _service.GetByIdAsync(id);
            if (appointment == null) return NotFound();

            appointment.Status = status;
            await _service.UpdateAsync(appointment);

            return RedirectToAction(nameof(Index));
        }


    }
}
