using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Items
{
    public class LinenArmor : Armor
    {
        /// <summary>
        /// Make this Item into a copy of the parameter Item
        /// </summary>
        /// <param name="desiredResult"></param>
        public void CopyItem(LinenArmor desiredResult)
        {
            this.ItemId = desiredResult.ItemId;
            this.Name = desiredResult.Name;
            this.Description = desiredResult.Description;
            this.Value = desiredResult.Value;
            this.IsSellable = desiredResult.IsSellable;
            this.IsEquiped = desiredResult.IsEquiped;
            this.ItemType = desiredResult.ItemType;
            this.Hp = desiredResult.Hp;
        }

        /// <summary>
        /// Return a new copy of this Item
        /// </summary>
        /// <returns></returns>
        public override Item CloneItem()
        {
            var output = new LinenArmor();
            output.ItemId = this.ItemId;
            output.Name = this.Name;
            output.ItemType = this.ItemType;
            output.IsEquiped = this.IsEquiped;
            output.Description = this.Description;
            output.Value = this.Value;
            output.Hp = this.Hp;
            output.IsSellable = this.IsSellable;
            return output;
        }

        public LinenArmor()
        {
            this.Name = "Linen Armor";
            this.Description = "Simple armor";
            this.Value = 1;
            this.ItemType = EItemType.Armor;
            this.IsSellable = true;
            this.Hp = 1;
        }
    }
}