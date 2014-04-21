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
            new Item{ItemName="AD", ItemType="Sword",ItemPrice=12.99M,ItemDescription="Blah",ItemID=1,ItemClass="Weapon"},
          new Item{ItemName="FD", ItemType="Helm",ItemPrice=12.99M,ItemDescription="Blah",ItemID=2,ItemClass="Armor"},
          new Item{ItemName="we", ItemType="Dagger",ItemPrice=12.99M,ItemDescription="Blah",ItemID=3,ItemClass="Weapon"},
            };

            items.ForEach(s => context.Items.Add(s));
            context.SaveChanges();
        }
    }
}