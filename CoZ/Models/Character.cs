using CoZ.Models.Items;
using CoZ.Models.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Gold { get; set; }
        public virtual ICollection<Item> Inventory { get; set; }
        public virtual Location CurrentLocation { get; set; }
        public virtual Map Map { get; set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        //Statistics
        public int Experience { get; set; }
        public int Level { get; set; }
        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }
        public int MaxMp { get; set; }
        public int CurrentMp { get; set; }
        public int Strength { get; set; }
        public int Magic { get; set; }
        public int Insanity { get; set; }
    }
}