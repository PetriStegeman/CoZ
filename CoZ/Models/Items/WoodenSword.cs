using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Items
{
    public class WoodenSword : Weapon
    {
        /// <summary>
        /// Make this Item into a copy of the parameter Item
        /// </summary>
        /// <param name="desiredResult"></param>
        public void CopyItem(WoodenSword desiredResult)
        {
            this.ItemId = desiredResult.ItemId;
            this.Name = desiredResult.Name;
            this.Description = desiredResult.Description;
            this.Value = desiredResult.Value;
            this.IsSellable = desiredResult.IsSellable;
            this.IsEquiped = desiredResult.IsEquiped;
            this.ItemType = desiredResult.ItemType;
            this.Strength = desiredResult.Strength;
        }

        /// <summary>
        /// Return a new copy of this Item
        /// </summary>
        /// <returns></returns>
        public override Item CloneItem()
        {
            var output = new WoodenSword();
            output.ItemId = this.ItemId;
            output.Name = this.Name;
            output.ItemType = this.ItemType;
            output.IsEquiped = this.IsEquiped;
            output.Description = this.Description;
            output.Value = this.Value;
            output.Strength = this.Strength;
            output.IsSellable = this.IsSellable;
            return output;
        }

        public WoodenSword()
        {
            this.Name = "Wooden Sword";
            this.Description = "Simple Sword";
            this.Value = 1;
            this.ItemType = EItemType.Weapon;
            this.IsSellable = true;
        }
    }
}