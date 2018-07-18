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

        private bool endNoteFound = false;
        private bool altitudeChanged = false;
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

            CleanUp();

            return map;
        }


        // returns a set of coördinates on which the river runs
        private RiverPath FindRiverPath(int coördinateX, int coördinateY, ICollection<Location> map)
        {
            //Sets up the system
            List<RiverPath> possibleRiverWays = Setup(coördinateX, coördinateY, map);

            //Starts searching for a way to a river, ocean or lake
            possibleRiverWays = StartSearchingForEndnode(possibleRiverWays,map);
            List<RiverPath> newRiverWays = new List<RiverPath>();
            foreach (RiverPath RiverPath in possibleRiverWays)
            {
                if (RiverPath.EndCheck(RiverPath.LastX(), RiverPath.LastY(), map))
                { newRiverWays.Add(RiverPath); }
            }

            SelectRandomPath(newRiverWays);
            return possibleRiverWays[0];
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
        
        //Starts searching for a way to a river, ocean or lake
        private List<RiverPath> StartSearchingForEndnode(List<RiverPath> allPaths, ICollection<Location> map)
        {
            possibleRiverWays = allPaths;

            do
            {
                List<RiverPath> newPaths = new List<RiverPath>();

                foreach (RiverPath oldPath in possibleRiverWays) // only uses the paths of the current lenght and skips the new paths
                {
                    newPaths = SearchNewPaths(oldPath.LastX(), oldPath.LastY(), oldPath, map); //roept de RiverSearch uit op een paar coördinaten vanuit de int[]
                    
                }

                foreach (RiverPath newPath in newPaths)
                { possibleRiverWays.Add(newPath); }

                possibleRiverWays = newPaths;
                currentRiverLength++;
                List<RiverPath> updatedlist = new List<RiverPath>();
                updatedlist = CleanRiverPaths(possibleRiverWays, map);
                possibleRiverWays = updatedlist;
                //possibleRiverWays = CleanRiverPaths(possibleRiverWays, map);
                if (altitudeChanged) { SelectRandomPath(possibleRiverWays); }      
                
                //break bij 0 nieuwer rivieren bij iteratie
                // recuring method?
                //break bij 20, zet meer tegel op een van de rivier lokaties en draai opnieuw met zelfde seed
            }
            while (!endNoteFound); // keeps running till an end of the river can be found

            return possibleRiverWays;
        }

        //Looks if any near tile is a suitable end node and returns all possible paths
        private List<RiverPath> SearchNewPaths(int coördinateX, int coördinateY, RiverPath localPath, ICollection<Location> map)
        {
            List<RiverPath> newPaths = new List<RiverPath>();
            var currentAltide = map.First(l => l.XCoord == coördinateX && l.YCoord == coördinateY).Altitude;
            if (localPath.MoveCheck(1 + coördinateX, coördinateY, currentRiverAltitude, map))
            { newPaths.Add(SeekPath(1 + coördinateX, coördinateY, localPath, map)); }
            if (localPath.MoveCheck(coördinateX - 1, coördinateY, currentRiverAltitude, map))
            { newPaths.Add(SeekPath(coördinateX - 1, coördinateY, localPath, map)); }
            if (localPath.MoveCheck(coördinateX, 1 + coördinateY, currentRiverAltitude, map))
            { newPaths.Add(SeekPath(coördinateX, 1 + coördinateY, localPath, map)); }
            if (localPath.MoveCheck(coördinateX, coördinateY - 1, currentRiverAltitude, map))
            { newPaths.Add(SeekPath(coördinateX, coördinateY - 1, localPath, map)); }

            return newPaths;
        }

        // removes all the old river paths and river paths that turn into themselves from the list
        private List<RiverPath> CleanRiverPaths(List<RiverPath> riverPaths, ICollection<Location> map)
        {

            List<RiverPath> newList = new List<RiverPath>();
            foreach (RiverPath coördinates in riverPaths)
            {
                if (CheckIfOldHighgroundOrInverting(riverPaths, coördinates, map))
                newList.Add(coördinates);
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

        // sets the PossibleWays list to only contain one of the possible ways, randomly
        private RiverPath SelectRandomPath(List<RiverPath> PossibleRiverPaths)
        {
            altitudeChanged = false;
            int randomInt = RngThreadSafe.Next(0, ((PossibleRiverPaths.Count == 0)?1:(PossibleRiverPaths.Count)));
            RiverPath chosenRiverPath = PossibleRiverPaths[randomInt];
            return chosenRiverPath; // selects a random possible route for the river
        }

        private RiverPath SeekPath(int coördinateX, int coördinateY, RiverPath oldPath, ICollection<Location> map)
        {
                if (oldPath.EndCheck(coördinateX, coördinateY, map))
                { endNoteFound = true; }
                if (map.First(l => l.XCoord == coördinateX && l.YCoord == coördinateY).Altitude < currentRiverAltitude)
                { altitudeChanged = true; }
                coördinate newCoördinate = new coördinate(coördinateX, coördinateY);
                RiverPath newRiverPath = new RiverPath(oldPath, newCoördinate);
                return newRiverPath;
        }

        //cleanes the RiverFactory up to pre usage state
        private void CleanUp()
        {
            endNoteFound = false;
            altitudeChanged = false;
            currentRiverLength = 1;
            currentRiverAltitude = 0;
            possibleRiverWays = null;
        }
    }
}