using CoZ.Models.Monsters;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Locations
{
    public class River : Location, IWater
    {
        public override Location CopyLocation()
        {
            var location = new River();
            location.LocationId = this.LocationId;
            location.XCoord = this.XCoord;
            location.YCoord = this.YCoord;
            location.Description = this.Description;
            location.ShortDescription = this.ShortDescription;
            location.IsVisited = this.IsVisited;
            location.Altitude = this.Altitude;
            return location;
        }

        public River(int x, int y)
        {
            this.XCoord = x;
            this.YCoord = y;
            this.Description = "You find yourself on the edge of a shallow river. " + RiverDescription();
            this.ShortDescription = "a river";
        }

        public River()
        {
            this.Description = "You find yourself on the edge of a shallow river. " + RiverDescription();
            this.ShortDescription = "a river";
        }

        private string RiverDescription()
        {
            switch (RngThreadSafe.Next(1, 5))
            {
                case 1:
                    return "The river is not very wide, it seems narrow enough to jump across. It doesn't hinder your journey in any way.";
                case 2:
                    return "The water flows rapidly, some fish swimming desperately against the strong current. If you step carefully, you think you can make it across without getting wet.";
                case 3:
                    return "The water is no deeper than your knee, and perhaps three or four paces across. The sound of the moving water is relaxing. Perhaps you should take a break...";
                default: return "Water water water. Why do you have to encounter water. You do NOT want to get wet, but sometimes you have to do things you don't like when you're on an adventure...";
            }
        }
    }
}