
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
        //20% chance to add Monster to Monsterlist of Location
        public static Monster CreateMonster(Location location)
        {
            if (RngThreadSafe.Next(1, 100) <= 50)
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
