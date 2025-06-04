using EstatePro.Data;
using EstatePro.Models;
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
        public IActionResult Index()
        {
            var data = transactionRepository.GetAllTransactions().ToList();
            return View(data);
        }
    }
}
