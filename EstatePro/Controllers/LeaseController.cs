using Microsoft.AspNetCore.Mvc;

namespace EstatePro.Controllers
{
    public class LeaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
