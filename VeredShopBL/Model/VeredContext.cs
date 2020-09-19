using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeredShopBL.VeredShopModel
{
    public class VeredContext : DbContext

    {
        public VeredContext() : base("VeredShopConnection") { }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sell> Sells { get; set; }

        public DbSet<Seller> Sellers { get; set; }

        public DbSet<Storekeeper> Storekeepers { get; set; }
    }
}
