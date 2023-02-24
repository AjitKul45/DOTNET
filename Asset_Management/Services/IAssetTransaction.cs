using Asset_Management.Models;

namespace Asset_Management.Services
{
    public interface IAssetTransaction
    {
        AssetTransaction getAssetTransactionByEmail(string email);
    }
}
