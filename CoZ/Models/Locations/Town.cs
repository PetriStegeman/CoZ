using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Locations
{
    public class Town : Location
    {
        public override Location CopyLocation()
        {
            var location = new Town();
            location.LocationId = this.LocationId;
            location.XCoord = this.XCoord;
            location.YCoord = this.YCoord;
            location.Description = this.Description;
            location.ShortDescription = this.ShortDescription;
            location.IsVisited = this.IsVisited;
            location.Altitude = this.Altitude;
            return location;
        }

        public Town(int x, int y)
        {
            this.XCoord = x;
            this.YCoord = y;
            this.Description = TownDescription();
            this.ShortDescription = "a town";
        }

        public Town()
        {
            this.Description = TownDescription();
            this.ShortDescription = "a town";
        }

        private string TownDescription()
        {
            switch (RngThreadSafe.Next(1, 5))
            {
                case 1:
                    return "You have reached the town of Argense. It is a small farming community with a sizeable market.";
                case 2:
                    return "You have reached the town of Liode. It is barely more than a few houses, but you might still find what you need.";
                case 3:
                    return "You have reached the town of Yuon. How people still call this a town is unbelievable. More houses than you can count spread out ahead of you.";
                default: return "You have reached the town of Nani. It seems like a nice quiet little town. You might want to settle down here sometime, once you killed a dragon.";
            }
        }
    }
}