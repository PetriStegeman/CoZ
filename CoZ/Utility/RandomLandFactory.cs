using CoZ.Models.FactoryItems;
using CoZ.Models.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Utility
{
    public static class RandomLandFactory
    {
        public static List<Location> CreateRandomLand(int xSize, int ySize, int landWaterRatio)
        {
            List < coordinate > map= FillTheSidesWithWater(xSize, ySize);


            int CenterX = xSize / 2;
            int CenterY = ySize / 2;
            coordinate CenterOfLandMass = new coordinate(CenterX, CenterY);
            coordinate NWCorner = new coordinate(2, 2);
            coordinate NECorner = new coordinate(2, ySize - 1);
            coordinate SECorner = new coordinate(xSize - 2, ySize - 1);
            coordinate SWCorner = new coordinate(xSize - 2, 2);
            
            Continent Zeelandia = new Continent(xSize, ySize, false);
            Continent Lemuria = new Continent(xSize, ySize, false);
            Continent Thule = new Continent(xSize, ySize, false);
            Continent Mu = new Continent(xSize, ySize, false);
            Continent Zork = new Continent(xSize, ySize, true);

            Zeelandia.SurfaceArea.Add(NWCorner);
            Lemuria.SurfaceArea.Add(NECorner);
            Thule.SurfaceArea.Add(SECorner);
            Mu.SurfaceArea.Add(SWCorner);
            Zork.SurfaceArea.Add(CenterOfLandMass);

            for (int i = 1; i < xSize*ySize; i++)
            {
                for (int j = 0; j < landWaterRatio; j++)
                {
                    AddNewTile(Zork, map);
                }

                AddNewTile(Zeelandia, map);
                AddNewTile(Lemuria, map);
                AddNewTile(Thule, map);
                AddNewTile(Mu, map);
            }

            

            return ReturnMap(Zork, map);
        }
        
        private static void AddNewTile(Continent continent, List<coordinate> map)
        {
            coordinate nullChecker = AddToContinent(continent, map);
            
            if(nullChecker != null)
            {
                if (!nullChecker.CheckCoordinateDuplicacy(map))
                {
                    continent.SurfaceArea.Add(nullChecker);
                    map.Add(nullChecker);
                }
            }
        }

        private static coordinate AddToContinent(Continent continent, List<coordinate> map)
        {
            coordinate newCoordinate = null;
            List<int> listOfChecks = fillListOfChecks(continent.SurfaceArea.Count());
            
            while (newCoordinate == null)
            {
                var tileToExpandFrom = NextTile(continent, listOfChecks);
                newCoordinate = SearchNewPaths(tileToExpandFrom, continent, map);
                if (listOfChecks.Count()<1)
                {
                    break;
                }
            }
            return newCoordinate;
            }


        private static List<int> fillListOfChecks(int count)
        {
            List<int> listOfChecks = new List<int>();
            for (int i = 0; i < count; i++)
            {
                listOfChecks.Add(i);
            }
            return listOfChecks;
        }

        private static List<coordinate> FillTheSidesWithWater(int xSize, int ySize)
        {
            List<coordinate> listOfCoordinates = new List<coordinate>();

            for (int x = 1; x <= xSize; x++)
            {
                for (int y = 1; y <= ySize; y++)
                {
                    if ((x == 1) || (y == 1) || (x == xSize) || (y == ySize))
                    {
                        listOfCoordinates.Add(new coordinate(x, y));
                    }
                }
            }

            return listOfCoordinates;
        }

        private static coordinate NextTile(Continent continent, List<int> listOfOptions)
        {
            var randomNumber = RngThreadSafe.Next(0, listOfOptions.Count);
            var tileToExpandFrom = continent.SurfaceArea[listOfOptions[randomNumber]];
            listOfOptions.RemoveAt(randomNumber);
            return tileToExpandFrom;
        }

        private static coordinate SearchNewPaths(coordinate coordinate, Continent continent, List<coordinate> map)
        {
            var coordX = coordinate.X;
            var coordY = coordinate.Y;
            List<coordinate> newPaths = new List<coordinate>();

            if (coordX != continent.MapXSize)
            {
                coordinate northCoordinate = new coordinate(coordX + 1, coordY);
                if (!map.Contains(northCoordinate))
                {
                    newPaths.Add(northCoordinate);
                }
            }
            if (coordX != 1)
            {
                coordinate southCoordinate = new coordinate(coordX - 1, coordY);
                if (!map.Contains(southCoordinate))
                {
                    newPaths.Add(southCoordinate);
                }
            }
            if (coordY != 1)
            {
                coordinate westCoordinate = new coordinate(coordX, coordY - 1);
                if (!map.Contains(westCoordinate))
                {
                    newPaths.Add(westCoordinate);
                }
            }
            if (coordY != continent.MapYSize)
            {
                coordinate eastCoordinate = new coordinate(coordX, coordY + 1);
                if (!map.Contains(eastCoordinate))
                {
                    newPaths.Add(eastCoordinate);
                }
            }

            if (newPaths.Count() == 0)
            {
                return null;
            }

            var randomIndex = RngThreadSafe.Next(0, newPaths.Count());
            var newLocation = newPaths[randomIndex];
            return newLocation;
        }

        private static List<Location> ReturnMap(Continent continent, List<coordinate> map)
        {
            List<Location> listOfLocations = new List<Location>();
            List<coordinate> cleanedList = continent.SurfaceArea.Where(x => x != null).ToList();
            cleanedList = cleanedList.Where(x => x.X != 1 && x.X!= continent.MapXSize && x.Y != 1 && x.Y != continent.MapYSize).ToList();
            List<coordinate> continentalLand = new List<coordinate>();

            foreach (coordinate coordinate in cleanedList)
            {
                if (!continentalLand.Contains(coordinate))
                {
                    continentalLand.Add(coordinate);
                }
            }

            foreach (coordinate coordinate in continentalLand)
            {
                listOfLocations.Add(new Plains(coordinate.X, coordinate.Y));
            }

            for (int x = 1; x <= continent.MapXSize; x++)
            {
                for (int y = 1; y <= continent.MapYSize; y++)
                {
                    if (listOfLocations.Where(r => r.XCoord == x && r.YCoord == y).Count() < 1)
                    {
                        listOfLocations.Add(new Ocean(x, y));
                    }
                }
            }

            List<Location> newMap = new List<Location>();
            foreach (Location location in listOfLocations)
            {
                if (newMap.Where(x => x.XCoord == location.XCoord && x.YCoord == location.YCoord).ToList().Count<1)
                {
                    newMap.Add(location);
                }
            }

            return newMap;
        }
    }
}