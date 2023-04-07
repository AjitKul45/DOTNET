using Asset_Management.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Asset_Management.Services
{
    public class AssetTransactionService : IService<AssetTransaction, int>,IAssetTransactionService<AssetTransaction,string>,IAssetTransaction
    {
        asset_managementContext ctx;
        public AssetTransactionService(asset_managementContext ctx)
        {
            this.ctx = ctx;
        }

        public Task<IEnumerable> GetStatus()
        {
            throw new NotImplementedException();
        }

        async Task<AssetTransaction> IService<AssetTransaction, int>.CreateAsync(AssetTransaction entity)
        {
            try
            {
                entity.UserId = null;
                var result = await ctx.AssetTransactions.AddAsync(entity);
                await ctx.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<AssetTransaction> IService<AssetTransaction, int>.DeleteAsync(int id)
        {
            var record = await ctx.AssetTransactions.FindAsync(id);
            if(record != null)
            {
                ctx.AssetTransactions.Remove(record);
                await ctx.SaveChangesAsync();
            }
            return record;
        }

        async Task<AssetTransaction> IAssetTransactionService<AssetTransaction, string>.getAssetTransactionByEmail(string email)
        {
            try
            {
                var record = ctx.AssetTransactions.Where(rec => rec.Email == email && rec.SubmitDate == null).FirstOrDefault();
                return record;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<IEnumerable<AssetTransaction>> IService<AssetTransaction, int>.GetAsync()
        {
            var result = await (ctx.AssetTransactions).ToListAsync();
            return result;
        }

        async Task<AssetTransaction> IService<AssetTransaction, int>.GetAsync(int pk)
        {
           var result = await (ctx.AssetTransactions).FindAsync(pk);
            return result;
        }

        async Task<IEnumerable<AssetTransaction>> IAssetTransactionService<AssetTransaction, string>.GetByDeptAsync(string dept)
        {
            try
            {
                var records = await ctx.AssetTransactions.Where(a=>a.Department.Equals(dept)).ToListAsync();
                return records;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<IEnumerable<AssetTransaction>> IAssetTransactionService<AssetTransaction, string>.GetByLocationtAsync(string location)
        {
            try
            {
                var records = await ctx.AssetTransactions.Where(a => a.Location.Equals(location)).ToListAsync();
                return records;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<AssetTransaction> IAssetTransactionService<AssetTransaction, string>.Search(string search)
        {
            try
            {
                var record = (await ctx.AssetTransactions.ToListAsync()).Where(a => a.UserName.Equals(search) || a.UserId == Convert.ToInt32(search) ||
                a.AssetId == Convert.ToInt32(search)).FirstOrDefault();
                return record;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<AssetTransaction> IService<AssetTransaction, int>.UpdateAsync(int id, AssetTransaction entity)
        {
            var result = await(ctx.AssetTransactions).FindAsync(id);
            if(result != null)
            {
                result = entity;
                //result.SubmitDate = entity.SubmitDate;
                //result.IssueDate = entity.IssueDate;
                //result.UserName = entity.UserName;
                //result.UserId = entity.UserId;
                //result.Email = entity.Email;
                //result.EmpId = entity.EmpId;
                //result.IssuedBy = entity.IssuedBy;
                //result.Location = entity.Location;
                //result.Department = entity.Department;

                await ctx.SaveChangesAsync();

            }
            return result;
        }



        async Task<IEnumerable> IAssetTransaction.GetDetailsOfTransactions()
        {
            var assets = await ctx.AssetDetails.ToListAsync();
            var trasactions = await ctx.AssetTransactions.ToListAsync();

            var detailsTransaction = from t in trasactions
                                     join a in assets on t.AssetId equals a.Id
                                     select new
                                     {
                                         assetId = a.Id,
                                         assetName = a.Name,
                                         assetType = a.Tyape,
                                         transactionId = t.Id,
                                         empId = t.EmpId,
                                         email = t.Email,
                                         username = t.UserName,
                                         location = t.Location,
                                         issueDate = t.IssueDate,
                                         issuedBy = t.IssuedBy,
                                         submitDate = t.SubmitDate,
                                         department = t.Department,
                                     };

            return detailsTransaction;
        }
    }
}
