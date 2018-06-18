/*using CoZ.Models;
using CoZ.Models.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        private bool EndNoteFound = false;
        private bool altitudeChanged = false;
        private int CurrentRiverLenght = 1;
        private int currentRiverAltide = 0;
        private Map mapHolder = null;
        private List<List<int[]>> PossibleRiverWays = null;

        //sets all the coördinates it gets from the FindRiver method to river tiles
        public void CreateRiver(int coördinateX, int coördinateY, Map map)
        {
            List<int[]> FinalRiverPath = FindRiverPath(coördinateX, coördinateY, map);
            Location[][] worldMap = map.WorldMap.ToArray();
            foreach (int[] coördinate in FinalRiverPath)
            {
                coördinateX = coördinate[0];
                coördinateY = coördinate[1];
                worldMap[coördinateX, coördinateY] = new River();
            }

            CleanUp();
        }


        // returns a set of coördinates on which the river runs
        private List<int[]> FindRiverPath(int coördinateX, int coördinateY, Map map)
        {
            //Sets up the system
            Setup(coördinateX, coördinateY, map);

            //Starts searching for a way to a river, ocean or lake
            StartSearchingForEndnode(PossibleRiverWays);

            foreach (List<int[]> RiverPath in PossibleRiverWays)
            {
                DiscardDeadEnds(RiverPath);
            }

            SelectRandomPath(PossibleRiverWays);
            return PossibleRiverWays[0];
        }

        private void Setup(int coördinateX, int coördinateY, Map map)
        {
            List<int[]> startingList = new List<int[]>(); // the first route creation
            int[] startingPosition = new int[] { coördinateX, coördinateY }; // the first coördinate of the first route
            startingList.Add(startingPosition);
            PossibleRiverWays.Add(startingList);
            mapHolder = map;
        }

        //Removes all the paths that don't end at an endnode
        private void DiscardDeadEnds(List<int[]> RiverPath)
        {
            int[] lastCoördinate = RiverPath[RiverPath.Count - 1];
            if ((mapHolder.WorldMap[lastCoördinate[0], lastCoördinate[1]] is River) | (mapHolder.WorldMap[lastCoördinate[0], lastCoördinate[1]] is Ocean) | (mapHolder.WorldMap[lastCoördinate[0], lastCoördinate[1]] is Lake))
            { PossibleRiverWays.Remove(RiverPath); }
        }

        //Starts searching for a way to a river, ocean or lake
        private void StartSearchingForEndnode(List<List<int[]>> allPaths)
        {
            do
            {
                foreach (List<int[]> oldPath in allPaths.Where(oldPath => oldPath.Count == CurrentRiverLenght)) // only uses the paths of the current lenght and skips the new paths
                {
                    SearchNewPaths(ArrayConverter(oldPath).Item1, ArrayConverter(oldPath).Item2, oldPath); //roept de RiverSearch uit op een paar coördinaten vanuit de int[]
                }

                CurrentRiverLenght++;
                CleanRiverPaths(PossibleRiverWays);
                if (altitudeChanged) { SelectRandomPath(PossibleRiverWays); }
            }
            while (!EndNoteFound); // keeps running till an end of the river can be found
        }

        //takes the int array appart and sends back the first and second number inside of the last array
        private Tuple<int, int> ArrayConverter(List<int[]> riverPath)
        {
            int[] lastCoördinates = riverPath[(riverPath.Count - 1)];
            int x = lastCoördinates[0];
            int y = lastCoördinates[1];
            Tuple<int, int> thisTuple = new Tuple<int, int>(x, y);
            return thisTuple;
        }

        //Looks if any near tile is a suitable end node and returns all possible paths
        private void SearchNewPaths(int coördinateX, int coördinateY, List<int[]> localPath)
        {
            var currentAltide = mapHolder.WorldMap[coördinateX, coördinateY].Altitude;
            SeekPath(1 + coördinateX, coördinateY, localPath);
            SeekPath(coördinateX - 1, coördinateY, localPath);
            SeekPath(coördinateX, 1 + coördinateY, localPath);
            SeekPath(coördinateX, coördinateY - 1, localPath);
        }

        // removes all the old river paths and river paths that turn into themselves from the list
        private void CleanRiverPaths(List<List<int[]>> riverPaths)
        {
            foreach (List<int[]> coördinates in riverPaths)
            {
                PurgeOldHighgroundInverting(riverPaths, coördinates);
            }
        }

        // removes the riverPath from the PossiblePaths if it is a old, higheraltitude or looping path
        private void PurgeOldHighgroundInverting(List<List<int[]>> riverPaths, List<int[]> riverPath)
        {
            if (riverPath.Count < CurrentRiverLenght) // checks the old
            { riverPaths.Remove(riverPath); }
            if (mapHolder.WorldMap[ArrayConverter(riverPath).Item1, ArrayConverter(riverPath).Item2].Altitude > currentRiverAltide) // pakt de lokatie van de map zoals aangegeven in de coördinaten
            { riverPaths.Remove(riverPath); } // removes all riverPaths that don't go downward
            if (CheckCoördinateDuplicacy(riverPath))     //checks for dublicate positions in the path
            { riverPaths.Remove(riverPath); }
        }

        //checks if a riverpath is looping by checking a coördinate is dublicate inside of the array of coördinates
        private bool CheckCoördinateDuplicacy(List<int[]> riverPath)
        {
            bool checker = false;
            foreach (int[] coördinate in riverPath)
            {
                if (riverPath.IndexOf(coördinate) != riverPath.LastIndexOf(coördinate)) // checks if searching for the object from the start and from the back returns the same position
                {
                    checker = true;
                    break;
                }
            }
            return checker;
        }

        // sets the PossibleWays list to only contain one of the possible ways, randomly
        private List<int[]> SelectRandomPath(List<List<int[]>> PossibleRiverPaths)
        {
            altitudeChanged = false;
            Random random = new Random();
            int randomInt = random.Next(0, PossibleRiverPaths.Count - 1);
            List<int[]> chosenRiverPath = PossibleRiverPaths[randomInt];
            return chosenRiverPath; // selects a random possible route for the river
        }

        //Looks if the new path is a viable path and adds it to the list if viable
        private void SeekPath(int coördinateX, int coördinateY, List<int[]> oldPath)
        {
            if ((coördinateX >= 0 && coördinateX < mapHolder.WorldMap.Length) && (coördinateY >= 0 && coördinateY < mapHolder.WorldMap.Rank) && (mapHolder.WorldMap[coördinateX, coördinateY].Altitude <= currentRiverAltide)) // checks if the move is invalid (out of bounds or higher Altitude)
            {
                if ((mapHolder.WorldMap[coördinateX, coördinateY] is River) | (mapHolder.WorldMap[coördinateX, coördinateY] is Ocean) | (mapHolder.WorldMap[coördinateX, coördinateY] is Lake))
                { EndNoteFound = true; }
                if (mapHolder.WorldMap[coördinateX, coördinateY].Altitude < currentRiverAltide)
                { altitudeChanged = true; }
                int[] newCoördinate = new int[] { coördinateX, coördinateY };
                List<int[]> newPath = new List<int[]>(oldPath)
                {
                    newCoördinate // adds the coördiante to the list that was used
                };
                PossibleRiverWays.Add(newPath); // adds the new route to the list of routes
            }
        }

        //cleanes the RiverFactory up to pre usage state
        private void CleanUp()
        {
            EndNoteFound = false;
            altitudeChanged = false;
            CurrentRiverLenght = 1;
            currentRiverAltide = 0;
            mapHolder = null;
            PossibleRiverWays = null;
        }
    }
}
*/