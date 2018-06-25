using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Items
{
    public class WoodenSword : Weapon
    {
        public WoodenSword()
        {
            this.Name = "A Wooden Sword";
            this.Description = "A crudely carved wooden sword. It's better than nothing.";
            this.Value = 1;
            this.CanBeWeapon = true;
            this.IsSellable = true;
        }
    }
}