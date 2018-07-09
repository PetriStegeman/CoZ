using CoZ.Models.Monsters;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Locations
{
    public class Mountain : Location
    {
        public override Location CopyLocation()
        {
            var location = new Mountain();
            location.LocationId = this.LocationId;
            location.XCoord = this.XCoord;
            location.YCoord = this.YCoord;
            location.Description = this.Description;
            location.ShortDescription = this.ShortDescription;
            location.IsVisited = this.IsVisited;
            location.Altitude = this.Altitude;
            return location;
        }

        public Mountain(int x, int y)
        {
            this.XCoord = x;
            this.YCoord = y;
            this.Description = "You find yourself standing on the slopes of a mountain. " + MountainDescription();
            this.ShortDescription = "a mountain";
        }

        public Mountain()
        {
            this.Description = "You find yourself standing on the slopes of a mountain. " + MountainDescription();
            this.ShortDescription = "a mountain";
        }

        private string MountainDescription()
        {
            switch (RngThreadSafe.Next(1, 5))
            {
                case 1:
                    return "The mountain's peak is barely visible ahead of you, partly hidden by the low hanging clouds. You can't cross it for sure, but if you travel around the mountain you think you can still go in every direction...";
                case 2:
                    return "Ahead of you is the beautiful snow covered peak. Smooth white steep slopes, impossible to climb. You will have to travel around the mountain...";
                case 3:
                    return "There isn't any one peak to speak off, and I guess you could barely call it a mountain. There are paths criss crossing all over the 'mountain', so there isn't really anything stopping you from moving on.";
                default: return "There are many shadowy holes hidden behind boulders and under prickly bushes. You start to wonder how deep some of those caves go, and what you might find inside those caves...";
            }
        }
    }
}