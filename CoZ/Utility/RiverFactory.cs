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
        //Searches the neares tile that's of a lower Altitude
        //Isn't allowed to go up in Altitude (check)
        //Searches for a route to another river, ocean or lake tile (check)

        private bool endNoteFound = false;
        private bool altitudeChanged = false;
        private int currentRiverLength = 1;
        private int currentRiverAltitude = 0;
        private ICollection<Location> mapHolder = null;
        private List<RiverPath> possibleRiverWays = null;

        //sets all the coördinates it gets from the FindRiver method to river tiles
        public void CreateRiver(int coördinateX, int coördinateY, ICollection<Location> map)
        {
            RiverPath FinalRiverPath = FindRiverPath(coördinateX, coördinateY, map);
            ICollection<Location> worldMap = map;
            foreach (coördinate coördinate in FinalRiverPath.riverCoördinates)
            {
                var tempLocation = worldMap.First(l => l.XCoord == coördinate.X && l.YCoord == coördinateY);
                tempLocation = new River();
            }

            CleanUp();
        }


        // returns a set of coördinates on which the river runs
        private RiverPath FindRiverPath(int coördinateX, int coördinateY, ICollection<Location> map)
        {
            //Sets up the system
            Setup(coördinateX, coördinateY, map);

            //Starts searching for a way to a river, ocean or lake
            StartSearchingForEndnode(possibleRiverWays);

            foreach (RiverPath RiverPath in possibleRiverWays)
            {
                DiscardDeadEnds(RiverPath);
            }

            SelectRandomPath(possibleRiverWays);
            return possibleRiverWays[0];
        }

        private void Setup(int coördinateX, int coördinateY, ICollection<Location> map)
        {
            RiverPath startingList = new RiverPath(); // the first route creation
            coördinate startingPosition = new coördinate(coördinateX, coördinateY); // the first coördinate of the first route
            startingList.riverCoördinates.Add(startingPosition);
            possibleRiverWays.Add(startingList);
            mapHolder = map;
        }

        //Removes all the paths that don't end at an endnode
        private void DiscardDeadEnds(RiverPath RiverPath)
        {
            if (RiverPath.EndCheck(RiverPath.LastX(), RiverPath.LastY(), mapHolder))
            { possibleRiverWays.Remove(RiverPath); }
        }

        //Starts searching for a way to a river, ocean or lake
        private void StartSearchingForEndnode(List<RiverPath> allPaths)
        {
            do
            {
                foreach (RiverPath oldPath in allPaths.Where(oldPath => oldPath.riverCoördinates.Count == currentRiverLength)) // only uses the paths of the current lenght and skips the new paths
                {
                    SearchNewPaths(oldPath.LastX(), oldPath.LastY(), oldPath); //roept de RiverSearch uit op een paar coördinaten vanuit de int[]
                }

                currentRiverLength++;
                CleanRiverPaths(possibleRiverWays);
                if (altitudeChanged) { SelectRandomPath(possibleRiverWays); }
            }
            while (!endNoteFound); // keeps running till an end of the river can be found
        }

        //Looks if any near tile is a suitable end node and returns all possible paths
        private void SearchNewPaths(int coördinateX, int coördinateY, RiverPath localPath)
        {
            var currentAltide = mapHolder.First(l => l.XCoord == coördinateX && l.YCoord == coördinateY).Altitude;
            SeekPath(1 + coördinateX, coördinateY, localPath);
            SeekPath(coördinateX - 1, coördinateY, localPath);
            SeekPath(coördinateX, 1 + coördinateY, localPath);
            SeekPath(coördinateX, coördinateY - 1, localPath);
        }

        // removes all the old river paths and river paths that turn into themselves from the list
        private void CleanRiverPaths(List<RiverPath> riverPaths)
        {
            foreach (RiverPath coördinates in riverPaths)
            {
                PurgeOldHighgroundInverting(riverPaths, coördinates);
            }
        }

        // removes the riverPath from the PossiblePaths if it is a old, higheraltitude or looping path
        private void PurgeOldHighgroundInverting(List<RiverPath> riverPaths, RiverPath riverPath)
        {
            if (riverPath.riverCoördinates.Count < currentRiverLength) // checks the old
            { riverPaths.Remove(riverPath); }
            if (mapHolder.First(l => l.XCoord == riverPath.LastX() && l.YCoord == riverPath.LastY()).Altitude > currentRiverAltitude) // pakt de lokatie van de map zoals aangegeven in de coördinaten
            { riverPaths.Remove(riverPath); } // removes all riverPaths that don't go downward
            if (CheckCoördinateDuplicacy(riverPath))     //checks for dublicate positions in the path
            { riverPaths.Remove(riverPath); }
        }

        //checks if a riverpath is looping by checking a coördinate is dublicate inside of the array of coördinates
        private bool CheckCoördinateDuplicacy(RiverPath riverPath)
        {
            bool checker = false;
            foreach (coördinate coördinate in riverPath.riverCoördinates)
            {
                if (riverPath.riverCoördinates.IndexOf(coördinate) != riverPath.riverCoördinates.LastIndexOf(coördinate)) // checks if searching for the object from the start and from the back returns the same position
                {
                    checker = true;
                    break;
                }
            }
            return checker;
        }

        // sets the PossibleWays list to only contain one of the possible ways, randomly
        private RiverPath SelectRandomPath(List<RiverPath> PossibleRiverPaths)
        {
            altitudeChanged = false;
            Random random = new Random();
            int randomInt = random.Next(0, PossibleRiverPaths.Count - 1);
            RiverPath chosenRiverPath = PossibleRiverPaths[randomInt];
            return chosenRiverPath; // selects a random possible route for the river
        }

        //Looks if the new path is a viable path and adds it to the list if viable
        private void SeekPath(int coördinateX, int coördinateY, RiverPath oldPath)
        {
            if (oldPath.MoveCheck(coördinateX, coördinateY, currentRiverAltitude, mapHolder)) // checks if the move is invalid (out of bounds or higher Altitude)
            {
                if (oldPath.EndCheck(coördinateX, coördinateY, mapHolder))
                { endNoteFound = true; }
                if (mapHolder.First(l => l.XCoord == coördinateX && l.YCoord == coördinateY).Altitude < currentRiverAltitude)
                { altitudeChanged = true; }
                coördinate newCoördinate = new coördinate(coördinateX, coördinateY);
                possibleRiverWays.Add(oldPath.ReturnNewRiverPath(newCoördinate)); // adds the new route to the list of routes
            }
        }

        //cleanes the RiverFactory up to pre usage state
        private void CleanUp()
        {
            endNoteFound = false;
            altitudeChanged = false;
            currentRiverLength = 1;
            currentRiverAltitude = 0;
            mapHolder = null;
            possibleRiverWays = null;
        }
    }
}