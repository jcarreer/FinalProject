using SkyrimShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyrimShop.DAL
{
    public class ItemInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ItemContext>
    {
        protected override void Seed(ItemContext context)
        {
            var items = new List<Item>
            {
            new Item{ItemName="AD", ItemType="Sword",ItemPrice=12.99M,ItemDescription="Blah",ItemID=1,QuantityItem=12},
          new Item{ItemName="FD", ItemType="Helm",ItemPrice=12.99M,ItemDescription="Blah",ItemID=2,QuantityItem=13},
          new Item{ItemName="we", ItemType="Dagger",ItemPrice=12.99M,ItemDescription="Blah",ItemID=3,QuantityItem=11},
            };

            items.ForEach(s => context.Items.Add(s));
            context.SaveChanges();
        }
    }
}