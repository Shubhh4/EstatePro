using EstatePro.Models;

namespace EstatePro.Repository
{
    public interface ITransactionRepository
    {
        List<Transaction> GetAllTransactions();
        List<Transaction> GetTransactionByUserId(int id);
    }
}
