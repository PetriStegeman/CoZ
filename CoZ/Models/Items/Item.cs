using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoZ.Models.Items
{
    public abstract class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public bool IsSellable { get; set; }
        public bool IsEquiped { get; set; }

        /// <summary>
        /// Make this Item into a copy of the parameter Item
        /// </summary>
        /// <param name="desiredResult"></param>
        public virtual void CopyItem(Item desiredResult)
        {
            this.ItemId = desiredResult.ItemId;
            this.Name = desiredResult.Name;
            this.Description = desiredResult.Description;
            this.Value = desiredResult.Value;
            this.IsSellable = desiredResult.IsSellable;
            this.IsEquiped = desiredResult.IsEquiped;
        }

        /// <summary>
        /// Return a new copy of this Item
        /// </summary>
        /// <returns></returns>
        public abstract Item CloneItem();
    }
}