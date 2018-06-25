using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Items
{
    public class LinenArmor : Armor
    {
        public LinenArmor()
        {
            this.Name = "A Linen Armor";
            this.Description = "A crudely carved wooden sword. It's better than nothing.";
            this.Value = 1;
            this.CanBeArmor = true;
            this.IsSellable = true;
        }
    }
}