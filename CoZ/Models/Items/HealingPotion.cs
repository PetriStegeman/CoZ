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
        public override void Consume(Character character)
        {
            if (character.CurrentHp <= character.MaxHp - 5)
            {
                character.CurrentHp += 5;
            }
            else
            {
                character.CurrentHp = character.MaxHp;
            }
        }

        /// <summary>
        /// Make this Item into a copy of the parameter Item
        /// </summary>
        /// <param name="desiredResult"></param>
        public void CopyItem(HealingPotion desiredResult)
        {
            this.ItemId = desiredResult.ItemId;
            this.Name = desiredResult.Name;
            this.Description = desiredResult.Description;
            this.Value = desiredResult.Value;
            this.IsSellable = desiredResult.IsSellable;
            this.IsEquiped = desiredResult.IsEquiped;
            this.PortionsRemaining = desiredResult.PortionsRemaining;
            this.ItemType = desiredResult.ItemType;
        }

        /// <summary>
        /// Return a new copy of this Item
        /// </summary>
        /// <returns></returns>
        public override Item CloneItem()
        {
            var output = new HealingPotion();
            output.ItemId = this.ItemId;
            output.Name = this.Name;
            output.PortionsRemaining = this.PortionsRemaining;
            output.IsEquiped = this.IsEquiped;
            output.Description = this.Description;
            output.Value = this.Value;
            output.ItemType = this.ItemType;
            output.IsSellable = this.IsSellable;
            return output;
        }

        public HealingPotion()
        {
            this.Name = "Healing Potion";
            this.Description = "Recover 5 Hp";
            this.Value = 1;
            this.PortionsRemaining = 3;
            this.IsSellable = true;
            this.ItemType = EItemType.Consumable;
        }
    }
}