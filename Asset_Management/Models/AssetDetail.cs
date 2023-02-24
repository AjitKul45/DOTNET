using System;
using System.Collections.Generic;

namespace Asset_Management.Models
{
    public partial class AssetDetail
    {
        public AssetDetail()
        {
            AssetTransactions = new HashSet<AssetTransaction>();
        }

        public int Id { get; set; }
        public string? Tyape { get; set; }
        public string? Name { get; set; }
        public string? Proprietary { get; set; }
        public string? Configuration { get; set; }
        public string? ServiceTag { get; set; }
        public string? Model { get; set; }
        public string? HostName { get; set; }
        public string? Oem { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? Owner { get; set; }
        public string? Remarks { get; set; }
        public string? Ram { get; set; }
        public int? VendorId { get; set; }

        public virtual Vendor? Vendor { get; set; }
        public virtual ICollection<AssetTransaction> AssetTransactions { get; set; }
    }
}
