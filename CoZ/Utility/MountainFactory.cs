using CoZ.Models;
using CoZ.Models.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Utility
{
    public class MountainFactory
    {
        //THIS CLASS IS NOT THREAD SAFE
        //THIS CLASS IT A WORK IN PROGRESS TO CREATE A MOUNTAIN OF SIZE 5 IN A RANDOM SHAPE
        //TODO MAKE WORK 
        void CreateMountainRange(int x, int y, Map map)
        {
            Location[][] worldMap = map.WorldMap.ToArray();
            worldMap[x][y] = new Mountain();                     //Create starting tile
            List<Location> mountains = new List<Location>();
            mountains.Add(worldMap[x][y]);                       //Add starting tile to list of whole mountain range
            for (int i = 0; i < RngMountain(4); i++)                //Mountain range will be a length of 1-4, decided by rng helper method RngMountain
            {
                NextTile(mountains, worldMap);                           //Select the next tile that will be used as the next starting point
            }
            //AddFoothills(mountains, map);                         //Add Foothills surrounding every mountain tile
        }

        //Select the next base tile the mountainrange will expand from
        void NextTile(List<Location> mountains, Location[][] map)
        {
            Location location = null;
            switch (RngMountain(mountains.Count))               //Let the mountain range expand from a random mountain that already belongs to the mountain range
            {
                case 1:
                    location = mountains[0];
                    CoordFinder(location, mountains, map);
                    break;
                case 2:
                    location = mountains[1];
                    CoordFinder(location, mountains, map);
                    break;
                case 3:
                    location = mountains[2];
                    CoordFinder(location, mountains, map);
                    break;
                default:
                    location = mountains[3];
                    CoordFinder(location, mountains, map);
                    break;
            }
        }

        //Find the coordinates of the tile that the mountainrange will expand from and call the method to add a mountain from that position
        void CoordFinder(Location location, List<Location> mountains, Location[][] map)
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map.Rank; j++)
                {
                    if (map[i][j] == location)
                    {
                        AddMountain(i, j, mountains, map);          //Use the coordinates of the found location and use them to add a mountain on an adjacent tile
                    }
                }
            }
        }

        //randomly select a location surrounding the tile you expand from, and make sure it does not already belong to the mountain range
        void AddMountain(int x, int y, List<Location> mountains, Location[][] map)
        {
            switch (RngMountain(4))                                         //1 in 4 random number
            {
                case 1:
                    if (!AddMountainCheck(x + 1, y, mountains, map))
                    {       //Will check if mountain can be added at the tile to the north, and will return true of the tile is added
                        goto case 2;
                    }                                                       //If not possible try next case.
                    break;
                case 2:
                    if (!AddMountainCheck(x - 1, y, mountains, map))
                    {       //Check if mountain can be added to the south
                        goto case 3;
                    }
                    break;
                case 3:
                    if (!AddMountainCheck(x, y + 1, mountains, map))
                    {       //To the east
                        goto case 3;
                    }
                    break;
                case 4:
                    if (!AddMountainCheck(x, y - 1, mountains, map))
                    {       //To the west
                        goto case 1;
                    }
                    break;
            }
            return;
        }

        //Return true and add mountain if it can be added to the mountain range
        bool AddMountainCheck(int x, int y, List<Location> mountains, Location[][] map)
        {
            if (x < 0 || x >= map.Length || y < 0 || y >= map.Rank)
            {
                return false;                                           //Prevent indexoutofbounds exception by not allowing the action of the index is out of bounds
            }
            else if (MountainChecker(mountains, map[x][y]))     //If the potential mountain tile is not part of the mountain range return true
            {
                map[x][y] = new Mountain();                     //Add a mountain to the map and the mountains list.
                mountains.Add(map[x][y]);
                return true;
            }
            else return false;                                           //Returns false if no mountain is added and the tile can not be extended unto
        }

        //Can square be used as a new mountain or is it already part of the range
        bool MountainChecker(List<Location> mountains, Location location)
        {
            foreach (Mountain mountain in mountains)     //Check for every Mountain that is part of the current mountainrange wether it matches the potential tile to create a new part of the mountain range
            {
                if (mountain == location)               //If the location is already a part of the mountain range, it can not extend there so it returns falls
                {
                    return false;
                }
            }
            return true;
        }

        //Return a random int between 1 and x
        //NOT THREADSAFE
        int RngMountain(int x)
        {
            Random rndm = new Random();
            int result = rndm.Next(1, x);
            return result;
        }
    }
}