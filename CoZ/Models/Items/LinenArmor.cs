using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Items
{
    public class LinenArmor : Armor
    {
        public void CopyItem(LinenArmor desiredResult)
        {
            this.ItemId = desiredResult.ItemId;
            this.Name = desiredResult.Name;
            this.Description = desiredResult.Description;
            this.Value = desiredResult.Value;
            this.IsSellable = desiredResult.IsSellable;
            this.IsEquiped = desiredResult.IsEquiped;
            this.CanBeArmor = desiredResult.CanBeArmor;
            this.Hp = desiredResult.Hp;
        }

        public override Item CloneItem()
        {
            var output = new LinenArmor();
            output.ItemId = this.ItemId;
            output.Name = this.Name;
            output.CanBeArmor = this.CanBeArmor;
            output.IsEquiped = this.IsEquiped;
            output.Description = this.Description;
            output.Value = this.Value;
            output.Hp = this.Hp;
            output.IsSellable = this.IsSellable;
            return output;
        }

        public LinenArmor()
        {
            this.Name = "A Linen Armor";
            this.Description = "A thin shirt made of linen. It's better than nothing.";
            this.Value = 1;
            this.CanBeArmor = true;
            this.IsSellable = true;
        }
    }
}