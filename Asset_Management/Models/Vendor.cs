using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Asset_Management.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            AssetDetails = new HashSet<AssetDetail>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ContactNo { get; set; }
        public string? Address { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        [JsonIgnore]
        public virtual ICollection<AssetDetail> AssetDetails { get; set; }
    }
}
