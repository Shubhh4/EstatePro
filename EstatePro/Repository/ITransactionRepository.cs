using EstatePro.Models;

namespace EstatePro.Repository
{
    public interface ITransactionRepository
    {
        List<Transaction> GetAllTransactions();
        Transaction GetTransactionById(int id);
    }
}
