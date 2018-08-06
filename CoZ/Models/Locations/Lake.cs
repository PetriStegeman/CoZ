using CoZ.Models.Monsters;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Locations
{
    public class Lake : Location, IWater
    {
        public override Location CopyLocation()
        {
            var location = new Lake();
            location.LocationId = this.LocationId;
            location.XCoord = this.XCoord;
            location.YCoord = this.YCoord;
            location.Description = this.Description;
            location.ShortDescription = this.ShortDescription;
            location.IsVisited = this.IsVisited;
            location.Altitude = this.Altitude;
            return location;
        }

        public Lake(int x, int y)
        {
            this.XCoord = x;
            this.YCoord = y;
            this.Description = "You find yourself on the edge of a lake. " + LakeDescription();
            this.ShortDescription = "a lake";
        }

        public Lake()
        {
            this.Description = "You find yourself on the edge of a lake. " + LakeDescription();
            this.ShortDescription = "a lake";
        }

        private string LakeDescription()
        {
            switch (RngThreadSafe.Next(1, 5))
            {
                case 1:
                    return "The waters look calm and inviting. On closer inspection the water looks crystal clear. Going for a short swim might be nice.";
                case 2:
                    return "It's more of a puddle really. Barely 5 paces across, and by your estimation no deeper than your waist.";
                case 3:
                    return "Dark murky waters span almost to the horizon. The water looks very deep. Better thread with care, there is no telling what is hiding in these waters.";
                default: return "The water looks perfectly still. Too still. Something is not right. You better move on quickly.";
            }
        }
    }
}