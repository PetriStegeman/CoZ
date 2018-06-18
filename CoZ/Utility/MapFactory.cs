using CoZ.Models;
using CoZ.Models.Locations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace CoZ.Utility
{
    public class MapFactory
    {
        public static Map CreateSmallMap(string id)
        {
            Map worldMap = new Map();
            worldMap.WorldMap = new Collection<Location[]>();
            for (int i = 0; i <= 20; i++)
            {
                Location[] location = new Location[20];
                worldMap.WorldMap.Add(location);
                for (int j = 0; j <= 20; j++)
                {
                    location[j] = GetTile();
                }
            }
            return worldMap;
        }

        //Generate a 2D array that functions as a map, 40x40
        public static Map CreateMediumMap(string id)
        {
            Map worldMap = new Map();
            worldMap.WorldMap = new Collection<Location[]>();
            for (int i = 0; i <= 40; i++)
            {
                Location[] location = new Location[40];
                worldMap.WorldMap.Add(location);
                for (int j = 0; j <= 40; j++)
                {
                    location[j] = GetTile();
                }
            }
            return worldMap;
        }

        //Generate a 2D array that functions as a map, 60x60
        public static Map CreateBigMap(string id)
        {
            Map worldMap = new Map();
            worldMap.WorldMap = new Collection<Location[]>();
            for (int i = 0; i <= 60; i++)
            {
                Location[] location = new Location[60];
                worldMap.WorldMap.Add(location);
                for (int j = 0; j < 60; j++)
                {
                    location[j] = GetTile();
                }
            }
            return worldMap;
        }

        private static Location GetTile()
        {
            Location result = null;
            switch (RngThreadSafe.Next(1,5))
            {
                case 1: result = new Forest(); break;
                case 2: result = new Plains(); break;
                case 3: result = new Forest(); break;
                case 4: result = new Forest(); break;
                default: result = new Forest(); break;
            }
            return result;
        }
    }
}