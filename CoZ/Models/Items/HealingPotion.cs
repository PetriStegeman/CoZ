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