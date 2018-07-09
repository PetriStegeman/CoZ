using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Locations
{
    public class Lair : Location
    {
        public override Location CopyLocation()
        {
            var location = new Lair();
            location.LocationId = this.LocationId;
            location.XCoord = this.XCoord;
            location.YCoord = this.YCoord;
            location.Description = this.Description;
            location.ShortDescription = this.ShortDescription;
            location.IsVisited = this.IsVisited;
            location.Altitude = this.Altitude;
            return location;
        }

        public Lair(int x, int y)
        {
            this.XCoord = x;
            this.YCoord = y;
            this.Description = "You find yourself standing at the entrance of the Dragon's Lair.";
            this.ShortDescription = "dragon lair";
        }

        public Lair()
        {
            this.Description = "You find yourself standing at the entrance of the Dragon's Lair.";
            this.ShortDescription = "dragon lair";
        }
    }
}