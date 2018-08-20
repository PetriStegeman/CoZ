using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.FactoryItems
{
    public class Continent
    {
        public List<coordinate> SurfaceArea { get; set; }
        public int MapXSize { get; set; }
        public int MapYSize { get; set; }
        public bool IsLandContinent { get; set; }

        public Continent()
        {
            this.SurfaceArea = new List<coordinate>();
        }

        public Continent(int mapSizeX, int mapSizeY, bool isLand)
        {
            this.SurfaceArea = new List<coordinate>();
            this.MapXSize = mapSizeX;
            this.MapYSize = mapSizeY;
            this.IsLandContinent = isLand;
        }

    }
}