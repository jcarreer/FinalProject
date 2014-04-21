using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkyrimShop.Models
{
    public class Item
    {
        public enum ItemClassType
        {
            Weapon,
            Armor
        }

        public int ItemID { get; set; }

        [Display(Name  = "Name")]
        public string ItemName { get; set; }
        
        [Display(Name = "Type")]
        public string ItemType { get; set; }
        
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal ItemPrice { get; set; }
        
        [Display(Name = "Description")]
        public string ItemDescription { get; set; }

        [Display(Name = "Class")]
        public ItemClassType ItemClass { get; set; }
    }
}