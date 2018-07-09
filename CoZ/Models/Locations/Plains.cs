using CoZ.Models.Items;
using CoZ.Models.Monsters;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Locations
{
    public class Plains : Location
    {
        public override Location CopyLocation()
        {
            var location = new Plains();
            location.LocationId = this.LocationId;
            location.XCoord = this.XCoord;
            location.YCoord = this.YCoord;
            location.Description = this.Description;
            location.ShortDescription = this.ShortDescription;
            location.IsVisited = this.IsVisited;
            location.Altitude = this.Altitude;
            return location;
        }

        public Plains(int x, int y)
        {
            this.XCoord = x;
            this.YCoord = y;
            this.Description = "You find yourself on widespread grasslands. " + PlainsDescription();
            this.ShortDescription = "a plains";
        }

        public Plains()
        {
            this.Description = "You find yourself on widespread grasslands. " + PlainsDescription();
            this.ShortDescription = "a plains";
        }

        private string PlainsDescription()
        {
            switch (RngThreadSafe.Next(1, 5))
            {
                case 1:
                    return "Ankle high grass spreads in every direction. There are signs of grazing herd animals everywhere, but no animal in sight. It makes you a bit uncomfortabel.";
                case 2:
                    return "Waist high grass grows wildly in every direction. You can see trails through the grass where animals and travelers have gone before you, but you better thread safely. You don't know what could be hiding in the grass...";
                case 3:
                    return "Grass everywhere. More grass than you can handle. It's so boring, reaching all the way to the horizon. Better move on quickly...";
                default: return "A pleasant breeze makes the grass move in hypnotizing swaying motions. It makes you want to sit down and just look at it for a while...";
            }
        }
    }
}