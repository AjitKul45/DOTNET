using Asset_Management.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq;
using System.Xml.Linq;

namespace Asset_Management.Services
{
    public class AssetDetailService : IService<AssetDetail, int>,IAssetDetailService<AssetDetail,string>
    {
        asset_managementContext ctx;


        public AssetDetailService(asset_managementContext db)
        {
            this.ctx = db;
        }

        public async Task<IEnumerable> GetAssetCount()
        {
            var list = (await ctx.AssetDetails.ToListAsync());

            var counts = from a in list
                         group a by a.Tyape into grp
                         select new
                         {
                             type = grp.Key,
                             count = grp.Count()
                         };

            return counts;
        }

        public async Task<int> GetCountOfNotAssignedAssets()
        {
            int?[] allAssets = (await ctx.AssetDetails.ToListAsync()).Select(a => a.Id).ToArray();
            int?[] assignedAssets = (await ctx.AssetTransactions.ToListAsync()).Select(a => a.AssetId).ToArray();

            var count = allAssets.Except(assignedAssets);

            return count.Count();
            }

        public Task<IEnumerable> GetStatus()
        {
            throw new NotImplementedException();
        }

        async Task<AssetDetail> IService<AssetDetail, int>.CreateAsync(AssetDetail entity)
        {
            try
            {
                var record = await ctx.AssetDetails.AddAsync(entity);
                await ctx.SaveChangesAsync();
                return record.Entity;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<AssetDetail> IService<AssetDetail, int>.DeleteAsync(int id)
        {
            try
            {
                var record = await ctx.AssetDetails.FindAsync(id);
                if (record != null)
                {
                    ctx.AssetDetails.Remove(record);
                    await ctx.SaveChangesAsync();
                    return record;
                }
                else
                {
                    return record;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<IEnumerable<AssetDetail>> IService<AssetDetail, int>.GetAsync()
        {
            try
            {
                var records = await ctx.AssetDetails.ToListAsync();

                if (records == null)
                {
                    throw new Exception("Records not Found");
                }
                else
                {
                    return records;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<AssetDetail> IService<AssetDetail, int>.GetAsync(int id)
        {
            try
            {
                var record = await ctx.AssetDetails.FindAsync(id);
                if (record == null)
                    throw new Exception("Record not found");
                return record;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<IEnumerable<AssetDetail>> IAssetDetailService<AssetDetail, string>.GetByTypeAsync(string type)
        {
            try
            {
                var records = await ctx.AssetDetails.Where(a => a.Tyape.Equals(type)).ToListAsync();
                return records;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<IEnumerable<AssetDetail>> IAssetDetailService<AssetDetail, string>.GetByVendorAsync(string vendor)
        {
            try
            {
                var records = await ctx.AssetDetails.Where(a=>a.VendorId == Convert.ToInt32(vendor)).ToListAsync();
                return records;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<AssetDetail> IService<AssetDetail, int>.UpdateAsync(int id, AssetDetail entity)
        {
            try
            {
                var record = await ctx.AssetDetails.FindAsync(id);
                if (record != null)
                {
                    record.Proprietary = entity.Proprietary;
                   
                    record.Configuration = entity.Configuration;
                    record.Name = entity.Name;
                    record.Remarks = entity.Remarks;
                    record.Ram = entity.Ram;
                    record.Oem= entity.Oem;
                    record.Owner = entity.Owner;
                    record.ServiceTag= entity.ServiceTag;
                    record.Model= entity.Model;
                    record.HostName= entity.HostName;
                    record.ExpiryDate= entity.ExpiryDate;
                    record.VendorId= entity.VendorId;
                    record.Tyape = entity.Tyape;
                    await ctx.SaveChangesAsync();
                    return record;
                }
                else
                {
                    return entity;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
