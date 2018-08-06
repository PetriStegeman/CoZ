using CoZ.Models.Monsters;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Locations
{
    public class Ocean : Location, IWater
    {
        public override Location CopyLocation()
        {
            var location = new Ocean();
            location.LocationId = this.LocationId;
            location.XCoord = this.XCoord;
            location.YCoord = this.YCoord;
            location.Description = this.Description;
            location.ShortDescription = this.ShortDescription;
            location.IsVisited = this.IsVisited;
            location.Altitude = this.Altitude;
            return location;
        }

        public Ocean()
        { }

        public Ocean(int x, int y)
        {
            this.XCoord = x;
            this.YCoord = y;
            this.Description = "You find yourself on the wide ocean. " + OceanDescription();
            this.ShortDescription = "a ocean";
        }

        private string OceanDescription()
        {
            switch (RngThreadSafe.Next(1, 5))
            {
                case 1:
                    return "The ocean is calm and it is quite nice to sea the fish swim besides you.";
                case 2:
                    return "The ocean is calm and it is quite nice to sea the fish swim besides you.";
                case 3:
                    return "The ocean is calm and it is quite nice to sea the fish swim besides you.";
                default: return "The ocean is calm and it is quite nice to sea the fish swim besides you.";
            }
        }
    }
}