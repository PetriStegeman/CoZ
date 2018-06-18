using CoZ.Models.Items;
using CoZ.Models.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Locations
{
    public abstract class Location
    {
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public bool IsVisited { get; set; }
        public int Altitude { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Monster> Monsters { get; set; }
    }
}