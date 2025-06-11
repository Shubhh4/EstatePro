using EstatePro.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EstatePro.Controllers
{
    public class TransactionController : Controller
    {
        ITransactionRepository transactionRepository;
        public TransactionController(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }
        public IActionResult getTransactionById()
        {
            //var UserRole = HttpContext.Session.GetString("UserRole");
            //if (UserRole != "Buyer") return Unauthorized();
            //var UserId = HttpContext.Session.GetInt32("UserId");
            //if (UserId == null) return NotFound();

            int UserId = (int)HttpContext.Session.GetInt32("UserId"); // comment this after sahil adds session
            var data = transactionRepository.GetTransactionByUserId(UserId);
            return View(data);
        }

        public IActionResult getAllTransactions()
        {
            //var UserRole = HttpContext.Session.GetString("UserRole");
            //if (UserRole != "Admin" && UserRole != "Agent") return Unauthorized();

            var data = transactionRepository.GetAllTransactions();
            return View(data);
        }
    }
}
