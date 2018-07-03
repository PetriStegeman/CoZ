using CoZ.Models.Items;
using CoZ.Models.Monsters;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Locations
{
    public class Forest : Location
    {
        public override Location CopyLocation()
        {
            var location = new Forest();
            location.LocationId = this.LocationId;
            location.XCoord = this.XCoord;
            location.YCoord = this.YCoord;
            location.Description = this.Description;
            location.ShortDescription = this.ShortDescription;
            location.IsVisited = this.IsVisited;
            location.Altitude = this.Altitude;
            return location;
        }

        public Forest(int x, int y)
        {
            this.XCoord = x;
            this.YCoord = y;
            this.Description = "You find yourself in a forest. " + ForestDescription();
            this.ShortDescription = "a forest";
        }

        public Forest()
        {
            this.Description = "You find yourself in a forest. " + ForestDescription();
            this.ShortDescription = "a forest";
        }

        private string ForestDescription()
        {
            switch (RngThreadSafe.Next(1, 5))
            {
                case 1: return "The trees are packed tightly together, so much so that there is barely any sunlight filtering through the trees.";
                case 2: return "Tall tree trunks cover your field of view, as far as you can see in any direction. At least the open mossy ground and small shrubberies give you a feeling of peace.";
                case 3: return "Dense pine trees everywhere. Needles cover the ground everywhere, be careful not to get any in your boots.";
                default: return "It's the perfect embodiment of everything a forest should be. You might want to remember this location.";
            }
        }
    }
}