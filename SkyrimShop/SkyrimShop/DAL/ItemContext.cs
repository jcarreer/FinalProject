using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SkyrimShop.Models;
using SkyrimShop.ViewModels;

namespace SkyrimShop.DAL
{
    public class ItemContext : DbContext

    {
        public ItemContext() : base("ItemContext") { }
        public DbSet<Item> Items { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<SkyrimShop.ViewModels.ShoppingCartViewModel> ShoppingCartViewModels { get; set; }
    }
}