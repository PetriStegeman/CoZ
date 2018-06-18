using CoZ.Models.Locations;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models
{
    public class Map
    {
        public int Id { get; set; }
        //public Location[,] WorldMap { get; set; }
        public ICollection<Location[]> WorldMap { get; set; }

        public Map(){}
    }
}