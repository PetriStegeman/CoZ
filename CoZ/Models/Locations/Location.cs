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
        public int altitude { get; set; }
        public Item[] Items { get; set; }
        public Monster[] Monsters { get; set; }
    }
}