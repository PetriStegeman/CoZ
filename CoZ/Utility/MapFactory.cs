using CoZ.Models;
using CoZ.Models.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Utility
{
    public class MapFactory
    {
        public static Map CreateSmallMap()
        {
            Map worldMap = new Map();
            worldMap.WorldMap = new Location[20, 20];
            for (int i = 0; i < worldMap.WorldMap.Length; i++)
            {
                for (int j = 0; j < worldMap.WorldMap.Length; j++)
                {

                    worldMap.WorldMap[i, j] = GetTile();
                }
            }
            return worldMap;
        }

        //Generate a 2D array that functions as a map, 40x40
        public static Map CreateMediumMap()
        {
            Map worldMap = new Map();
            worldMap.WorldMap = new Location[40, 40];
            for (int i = 0; i < worldMap.WorldMap.Length; i++)
            {
                for (int j = 0; j < worldMap.WorldMap.Length; j++)
                {

                    worldMap.WorldMap[i, j] = GetTile();
                }
            }
            return worldMap;
        }

        //Generate a 2D array that functions as a map, 60x60
        public static Map CreateBigMap()
        {
            Map worldMap = new Map();
            worldMap.WorldMap = new Location[60, 60];
            for (int i = 0; i < worldMap.WorldMap.Length; i++)
            {
                for (int j = 0; j < worldMap.WorldMap.Length; j++)
                {

                    worldMap.WorldMap[i, j] = GetTile();
                }
            }
            return worldMap;
        }

        private static Location GetTile()
        {
            Location result = null;
            Random rnd = new Random();
            switch (rnd.Next(1, 5))
            {
                case 1: result = new Forest(); break;
                case 2: result = new Forest(); break;
                case 3: result = new Forest(); break;
                case 4: result = new Forest(); break;
                case 5: result = new Forest(); break;
            }
            return result;
        }
    }
}