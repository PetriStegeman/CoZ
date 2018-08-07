using CoZ.Models.Items;
using CoZ.Models.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Locations
{
    public abstract class Location
    {
        public int LocationId { get; set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public bool IsVisited { get; set; }
        public int Altitude { get; set; }
        public virtual Item Item { get; set; }
        public virtual Monster Monster { get; set; }
        public virtual Character Character { get; set; }

        public void CloneLocation(Location desiredResult)
        {
            this.LocationId = desiredResult.LocationId;
            this.XCoord = desiredResult.XCoord;
            this.YCoord = desiredResult.YCoord;
            this.Description = desiredResult.Description;
            this.ShortDescription = desiredResult.ShortDescription;
            this.IsVisited = desiredResult.IsVisited;
            this.Altitude = desiredResult.Altitude;
        }

        public abstract Location CopyLocation();

        public override string ToString()
        {
            return this.ShortDescription;
        }
    }
}