using Asset_Management.Models;
using System.Collections;

namespace Asset_Management.Services
{
    public interface IAssetTransaction
    {
        //AssetTransaction getAssetTransactionByEmail(string email);

        Task<IEnumerable> GetDetailsOfTransactions();
    }
}
