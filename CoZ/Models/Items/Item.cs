using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using System;
using System.Collections.Generic;
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
        public virtual Monster Monster { get; set; }
        public virtual Character Character { get; set; }
        public virtual Location Location { get; set; }
    }
}