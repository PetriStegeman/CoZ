using CoZ.Models.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.ViewModels
{
    public class LocationViewModel
    {
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string MonsterToNorth { get; set; }
        public string MonsterToEast { get; set; }
        public string MonsterToSouth { get; set; }
        public string MonsterToWest { get; set; }

        public LocationViewModel() { }

        public LocationViewModel(Location location, bool isMonsterNorth, bool isMonsterEast, bool isMonsterSouth, bool isMonsterWest)
        {
            this.Description = location.Description;
            this.ShortDescription = location.Description;
            this.MonsterToNorth = IsMonsterToNorth(isMonsterNorth);
            this.MonsterToEast = IsMonsterToEast(isMonsterEast);
            this.MonsterToSouth = IsMonsterToSouth(isMonsterSouth);
            this.MonsterToWest = IsMonsterToWest(isMonsterWest);
        }

        public string IsMonsterToNorth(bool isMonsterNorth)
        {
            if (isMonsterNorth == true)
            {
                return "You hear noice coming from the North. There might be monsters there...";
            }
            else
            {
                return "All is quiet to the North.";
            }
        }

        public string IsMonsterToEast(bool isMonsterEast)
        {
            if (isMonsterEast == true)
            {
                return "You hear noice coming from the East. There might be monsters there...";
            }
            else
            {
                return "All is quiet to the East.";
            }
        }

        public string IsMonsterToSouth(bool isMonsterSouth)
        {
            if (isMonsterSouth == true)
            {
                return "You hear noice coming from the South. There might be monsters there...";
            }
            else
            {
                return "All is quiet to the South.";
            }
        }

        public string IsMonsterToWest(bool isMonsterWest)
        {
            if (isMonsterWest == true)
            {
                return "You hear noice coming from the West. There might be monsters there...";
            }
            else
            {
                return "All is quiet to the West.";
            }
        }

    }
}