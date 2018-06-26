
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
            if (RngThreadSafe.Next(1, 100) <= 20)
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
                    break;
                case 2: return new KoboldWarrior(location);
                    break;
                case 3: return new KoboldGatherer(location);
                    break;
                case 4: return new KoboldHunter(location);
                    break;
                default: return new Deer(location);
            }
        }
    }
}
