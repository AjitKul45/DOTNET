using Asset_Management.Models;
using Asset_Management.Services;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Asset_Management.Services
{
    public class VendorService : IService<Vendor, int>
    {
        asset_managementContext ctx;

        public VendorService(asset_managementContext ctx) {
           this.ctx = ctx;
        }
        public  async Task<Vendor> CreateAsync(Vendor entity)
        {
            try
            {
                var cat = (await ctx.Vendors.ToListAsync()).Where(c => c.Id == entity.Id).FirstOrDefault();
                if (cat != null)
                    throw new Exception($"CatedoryId : {entity.Id} is already exist");
                var record = await ctx.Vendors.AddAsync(entity);
                await ctx.SaveChangesAsync();
                return record.Entity;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Vendor> DeleteAsync(int id)
        {
            try
            {
                var record = await ctx.Vendors.FindAsync(id);
                if (record == null)
                    throw new Exception("Record is not found");
                ctx.Vendors.Remove(record);
                await ctx.SaveChangesAsync();
                return record;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Vendor>> GetAsync()
        {
            return await ctx.Vendors.ToListAsync();
        }

        public async Task<Vendor> GetAsync(int id)
        {
            try
            {
                var record = await ctx.Vendors.FindAsync(id);
                if (record == null)
                    throw new Exception("Record not found");
                return record;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Vendor> UpdateAsync(int id, Vendor entity)
        {
            try
            {
                var record = await ctx.Vendors.FindAsync(id);
                if (record == null)
                    throw new Exception("Record ot found");
                record.ContactNo = entity.ContactNo;
                record.Name= entity.Name;
                record.RegistrationDate = entity.RegistrationDate;
                record.TerminationDate = entity.TerminationDate;
                record.Address = entity.Address;
                await ctx.SaveChangesAsync();
                return record;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
