﻿using CoZ.Models.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.ViewModels
{
    public class MapViewModel
    {
        public ICollection<Location> Locations { get; set; }
        public int CharXCoord { get; set; }
        public int CharYCoord { get; set; }


        public MapViewModel() { }

        public MapViewModel(ICollection<Location> Map, int xCoord, int yCoord)
        {
            this.Locations = Map;
            this.CharXCoord = xCoord;
            this.CharYCoord = yCoord;
        }
    }

}