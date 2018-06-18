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
        public int LocationId { get; set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public bool IsVisited { get; set; }
        public int Altitude { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Monster> Monsters { get; set; }
        //public virtual Character Character { get; set; }

        public abstract Monster AddMonster();
        //public abstract Item AddItem();
    }
}