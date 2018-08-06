
using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Utility
{
    public static class MonsterFactory
    {
        private const int _noChance = 1;
        private const int _fullChance = 100;
        private const int percentChance = 50;
        
        /// <summary>
        /// Randomly creates monster based on percentage value
        /// </summary>
        /// <param name="location">location to add the monster to</param>
        /// <returns></returns>
        public static Monster CreateMonster(Location location)
        {
            if (RngThreadSafe.Next(_noChance, _fullChance) <= percentChance)
            {
                return GetMonster(location);
            }
            return null; 
        }

        //Generate random new Monster
        private static Monster GetMonster(Location location)
        {
            switch (RngThreadSafe.Next(1, 6))
            {
                case 1: return new Boar(location);
                case 2: return new KoboldWarrior(location);
                case 3: return new KoboldGatherer(location);
                case 4: return new KoboldHunter(location);
                default: return new Deer(location);
            }
        }
    }
}
