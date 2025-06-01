using EstatePro.Models;
using EstatePro.Repository;
using Microsoft.AspNetCore.Mvc;


namespace EstatePro.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IProperty d;
        public PropertyController(IProperty d)
        {
            this.d = d;
        }
        public IActionResult Index()
        {
            List<Property> das= d.prp();
            return View(das);
        }
        public IActionResult AddProperty()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult AddProperty(Property p)
        {
            d.AddProperty(p);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            d.Delete(id);
            return RedirectToAction("Index");

        }
        public IActionResult Edit(int id)
        {
           var em= d.Edit(id);
            return View(em);
        }
        [HttpPost]
        public IActionResult Edit(Property p)
        {
            d.Edit(p);
            return RedirectToAction("Index");
        }
    }
}
