using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Items
{
    public class HealingPotion : Potion
    {
        public override void Consume()
        {
            //TODO Make do something
        }

        public void CopyItem(HealingPotion desiredResult)
        {
            this.ItemId = desiredResult.ItemId;
            this.Name = desiredResult.Name;
            this.Description = desiredResult.Description;
            this.Value = desiredResult.Value;
            this.IsSellable = desiredResult.IsSellable;
            this.IsEquiped = desiredResult.IsEquiped;
            this.PortionsRemaining = desiredResult.PortionsRemaining;
            this.CanBeConsumed = desiredResult.CanBeConsumed;
        }

        public override Item CloneItem()
        {
            var output = new HealingPotion();
            output.ItemId = this.ItemId;
            output.Name = this.Name;
            output.PortionsRemaining = this.PortionsRemaining;
            output.IsEquiped = this.IsEquiped;
            output.Description = this.Description;
            output.Value = this.Value;
            output.CanBeConsumed = this.CanBeConsumed;
            output.IsSellable = this.IsSellable;
            return output;
        }

        public HealingPotion()
        {
            this.Name = "Healing Potion";
            this.Description = "A small glass bottle, holding 3 portions of healing potion which recover 5 of your health points";
            this.Value = 1;
            this.PortionsRemaining = 3;
            this.IsSellable = true;
        }
    }
}