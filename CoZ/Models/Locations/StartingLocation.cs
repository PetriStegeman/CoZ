using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoZ.Models.Monsters;
using CoZ.Utility;

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

        public StartingLocation(int x, int y)
        {
            this.XCoord = x;
            this.YCoord = y;
            this.Description = "Hidden deep in the forest is a small cabin. For the past 20 years you've called this cabin your home. You know these woods like the back of your palm. This morning, something is off. You find a note, pinned to your door with an arrow. WANTED: DRAGON KRALTOCK, 5000 GOLD REWARD. Say no more. It's dragon slaying time.";
            this.ShortDescription = "your cabin";
        }

        public StartingLocation()
        {
            this.Description = "Hidden deep in the forest is a small cabin. For the past 20 years you've called this cabin your home. You know these woods like the back of your palm. This morning, something is off. You find a note, pinned to your door with an arrow. WANTED: DRAGON KRALTOCK, 5000 GOLD REWARD. Say no more. It's dragon slaying time.";
            this.ShortDescription = "your cabin";
        }
    }
}