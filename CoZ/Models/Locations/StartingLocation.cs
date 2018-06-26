using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoZ.Models.Monsters;

namespace CoZ.Models.Locations
{
    public class StartingLocation : Location
    {
        public override Location CopyLocation()
        {
            var location = new StartingLocation();
            location.LocationId = this.LocationId;
            location.XCoord = this.XCoord;
            location.YCoord = this.YCoord;
            location.Description = this.Description;
            location.ShortDescription = this.ShortDescription;
            location.IsVisited = this.IsVisited;
            location.Altitude = this.Altitude;
            return location;
        }
    }
}