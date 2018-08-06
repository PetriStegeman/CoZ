using CoZ.Models;
using CoZ.Models.FactoryItems;
using CoZ.Models.Locations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoZ.Utility
{
    public class RiverFactory
    {
        //DEZE CLASS IS NIET THREAD SAFE
        //TODO MAAK THREAD SAFE

        //River Logic Demands:
        //Starts at the source (check)
        //Searches the neares tile that's of a lower 

        //Isn't allowed to go up in Altitude (check)
        //Searches for a route to another river, ocean or lake tile (check)

        public RiverFactory()
        {
                
        }

        private bool endNodeFound = false;
        private bool altitudeChanged = false;
        private bool madeALake = false;
        coördinate lakeCoördinate;
        private int currentRiverLength = 0;
        private int currentRiverAltitude = 0;
        private List<RiverPath> possibleRiverWays = new List<RiverPath>();

        //sets all the coördinates it gets from the FindRiver method to river tiles
        public ICollection<Location> CreateRiver(int coördinateX, int coördinateY, ICollection<Location> map)
        {
            RiverPath FinalRiverPath = FindRiverPath(coördinateX, coördinateY, map);
            FinalRiverPath.riverCoördinates.RemoveAt(FinalRiverPath.riverCoördinates.Count - 1);
            
            foreach (coördinate coördinate in FinalRiverPath.riverCoördinates)
            {
                var tempLocation = map.Single(l => l.XCoord == coördinate.X && l.YCoord == coördinate.Y);
                map.Remove(tempLocation);
                var newRiverTile = new River(coördinate.X,coördinate.Y);
                map.Add(newRiverTile);
            }

            if (madeALake)
            {
                map = MakeALake(lakeCoördinate, map);
            }

            CleanUp();

            return map;
        }

        private ICollection<Location> MakeALake(coördinate lakeCoördinate, ICollection<Location> map)
        {
            var tempLocation = map.Single(l => l.XCoord == lakeCoördinate.X && l.YCoord == lakeCoördinate.Y);
            map.Remove(tempLocation);
            var ñêwLäkè = new Lake(lakeCoördinate.X, lakeCoördinate.Y);
            map.Add(ñêwLäkè);

            return map;
        }


        // returns a set of coördinates on which the river runs
        private RiverPath FindRiverPath(int coördinateX, int coördinateY, ICollection<Location> map)
        {
            //Sets up the system
            List<RiverPath> possibleRiverWays = Setup(coördinateX, coördinateY, map);

            //Starts searching for a way to a river, ocean or lake
            possibleRiverWays = RecursiveRiverCreator(possibleRiverWays,map);
            List<RiverPath> newRiverWays = new List<RiverPath>();
            foreach (RiverPath RiverPath in possibleRiverWays)
            {
                if (RiverPath.EndCheck(RiverPath.LastX(), RiverPath.LastY(), map))
                { newRiverWays.Add(RiverPath); }
            }

            //SelectRandomPath(newRiverWays);
            return newRiverWays[0];
        }

        private List<RiverPath> Setup(int coördinateX, int coördinateY, ICollection<Location> map)
        {
            RiverPath startingPath = new RiverPath(); // the first route creation 
            coördinate startingPosition = new coördinate(coördinateX, coördinateY); // the first coördinate of the first route
            startingPath.riverCoördinates.Add(startingPosition);
            currentRiverAltitude = map.First(l => l.XCoord == coördinateX && l.YCoord == coördinateY).Altitude;
            List<RiverPath> startingList = new List<RiverPath>();
            startingList.Add(startingPath);
            return startingList;
        }
        
        private List<RiverPath> RecursiveRiverCreator(List<RiverPath> inputList, ICollection<Location> inputMap)
        {
            List<RiverPath> RecursiveRiverList = new List<RiverPath>();
            RecursiveRiverList = createNewRiverBranches(inputList, inputMap, RecursiveRiverList);

            if (RecursiveRiverList.Count == 0)
            {
                return NoEndPointFound(inputList, inputMap);
            }
            if (endNodeFound)
            {
                List<RiverPath> returnPath = new List<RiverPath>();
                //doSomehting;                
                returnPath = RecursiveRiverList.Where(l => l.hasAnEndNode == true).ToList<RiverPath>();
                //RecursiveRiverList = CleanRiverPaths(returnPath, inputMap);
                if (returnPath.Count == 0)
                {
                    System.Console.WriteLine("error");
                }
                return returnPath;
            }
            if (altitudeChanged)
            {
                altitudeChanged = false;
                List<RiverPath> newList = CleanRiverPaths(RecursiveRiverList, inputMap);
                List<RiverPath> updatedList = new List<RiverPath>();
                if (newList.Count == 0)
                {
                    System.Console.WriteLine("error");
                }
                updatedList.Add(SelectRandomPath(newList));
                return RecursiveRiverCreator(updatedList, inputMap);
            }
            else
            {
                currentRiverLength++;
                RecursiveRiverList = CleanRiverPaths(RecursiveRiverList, inputMap);
                if (RecursiveRiverList.Count == 0)
                {
                    System.Console.WriteLine("error");
                }
                return RecursiveRiverCreator(RecursiveRiverList, inputMap);
            }
        }

        private List<RiverPath> NoEndPointFound(List<RiverPath> inputList, ICollection<Location> inputMap)
        {
            coördinate lastCoördinate = inputList[0].riverCoördinates[inputList[0].riverCoördinates.Count - 1];
            lakeCoördinate = lastCoördinate;

            inputMap = MakeALake(lastCoördinate, inputMap);

            coördinate firstCoördinate = inputList[0].riverCoördinates[0];
            List<RiverPath> possibleRiverWays = Setup(firstCoördinate.X, firstCoördinate.Y, inputMap);

            madeALake = true;
            return RecursiveRiverCreator(possibleRiverWays, inputMap);
        }

        private List<RiverPath> createNewRiverBranches(List<RiverPath> inputList, ICollection<Location> inputMap, List<RiverPath> RecursiveRiverList)
        {
            List<RiverPath> tempList = new List<RiverPath>();
            foreach (RiverPath oldPath in inputList) // only uses the paths of the current lenght and skips the new paths
            {
                List<RiverPath> newPaths = SearchNewPaths(oldPath, inputMap); //roept de RiverSearch uit op een paar coördinaten vanuit de int[]                    
                foreach (RiverPath newPath in newPaths)
                {
                    tempList.Add(newPath);
                }
            }

            return tempList;
        }

        //Looks if any near tile is a suitable end node and returns all possible paths
        private List<RiverPath> SearchNewPaths(RiverPath localPath, ICollection<Location> map)
        {
            var coordX = localPath.LastX();
            var coordY = localPath.LastY();
            List<RiverPath> newPaths = new List<RiverPath>();
            currentRiverAltitude = map.First(l => l.XCoord == coordX && l.YCoord == coordY).Altitude;
            if (localPath.MoveCheck(1 + coordX, coordY, currentRiverAltitude, map))
            { newPaths.Add(AddPath(1 + coordX, coordY, localPath, map)); }
            if (localPath.MoveCheck(coordX - 1, coordY, currentRiverAltitude, map))
            { newPaths.Add(AddPath(coordX - 1, coordY, localPath, map)); }
            if (localPath.MoveCheck(coordX, 1 + coordY, currentRiverAltitude, map))
            { newPaths.Add(AddPath(coordX, 1 + coordY, localPath, map)); }
            if (localPath.MoveCheck(coordX, coordY - 1, currentRiverAltitude, map))
            { newPaths.Add(AddPath(coordX, coordY - 1, localPath, map)); }

            return newPaths;
        }

        // removes all the old river paths and river paths that turn into themselves from the list
        private List<RiverPath> CleanRiverPaths(List<RiverPath> riverPaths, ICollection<Location> map)
        {

            List<RiverPath> newList = new List<RiverPath>();
            foreach (RiverPath coördinates in riverPaths)
            {
                if (CheckIfOldHighgroundOrInverting(riverPaths, coördinates, map))
                {
                    newList.Add(coördinates);
                }
            }
            return newList;
        }

        // removes the riverPath from the PossiblePaths if it is a old, higheraltitude or looping path
        private bool CheckIfOldHighgroundOrInverting(List<RiverPath> riverPaths, RiverPath riverPath, ICollection<Location> map)
        {
            if (riverPath.riverCoördinates.Count < currentRiverLength) // checks the old
            { return false; }
            else if (map.First(l => l.XCoord == riverPath.LastX() && l.YCoord == riverPath.LastY()).Altitude > currentRiverAltitude) // pakt de lokatie van de map zoals aangegeven in de coördinaten
            { return false; } // removes all riverPaths that don't go downward
            else if (CheckCoördinateDuplicacy(riverPath))     //checks for dublicate positions in the path
            { return false; }
            else
            { return true; }
        }

        //checks if a riverpath is looping by checking a coördinate is dublicate inside of the array of coördinates
        private bool CheckCoördinateDuplicacy(RiverPath riverPath)
        {
            foreach (coördinate coördinate in riverPath.riverCoördinates)
            {
                if (riverPath.riverCoördinates.Where(r => r.X == coördinate.X && r.Y == coördinate.Y).Count()>1) // checks if searching for the object from the start and from the back returns the same position
                {
                    return true;
                }
            }
            return false;
        }

        private RiverPath SelectRandomPath(List<RiverPath> PossibleRiverPaths)
        {
            altitudeChanged = false;
            int randomInt = (PossibleRiverPaths.Count == 0) ? 0:RngThreadSafe.Next(0, (PossibleRiverPaths.Count));
            RiverPath chosenRiverPath = PossibleRiverPaths[randomInt];
            return chosenRiverPath;
        }

        private RiverPath AddPath(int coördinateX, int coördinateY, RiverPath oldPath, ICollection<Location> map)
        {
            bool endNodeFoundChecker = false;            
            coördinate newCoördinate = new coördinate(coördinateX, coördinateY);
            RiverPath newRiverPath = new RiverPath(oldPath, newCoördinate);

            if (oldPath.EndCheck(coördinateX, coördinateY, map))
            {
                endNodeFound = true;
                endNodeFoundChecker = true;
            }
            if (map.First(l => l.XCoord == coördinateX && l.YCoord == coördinateY).Altitude < currentRiverAltitude)
            {
                altitudeChanged = true;
            }
            if (endNodeFoundChecker)
            {
                newRiverPath.hasAnEndNode = true;
            }
            return newRiverPath;
        }

        private void CleanUp()
        {
            endNodeFound = false;
            altitudeChanged = false;
            madeALake = false;
            currentRiverLength = 1;
            currentRiverAltitude = 0;
            possibleRiverWays = null;
        }
    }
}