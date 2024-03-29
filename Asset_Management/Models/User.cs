﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Asset_Management.Models
{
    public partial class User
    {
        public User()
        {
            AssetTransactions = new HashSet<AssetTransaction>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public virtual ICollection<AssetTransaction> AssetTransactions { get; set; }
    }
}
