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
            //this.Items = new List<Item>();
            this.Description = "You find yourself in rolling plains";
            this.ShortDescription = "a plains";
        }
        public Plains()
        {
            //this.Items = new List<Item>();
            this.Description = "You find yourself in rolling plains";
            this.ShortDescription = "a plains";
        }
    }
}