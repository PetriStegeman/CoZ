
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
        public static Monster CreateMonster()
        {
            if (RngThreadSafe.Next(1, 100) <= 20)
            {
                return GetMonster();
            }
            return null; 
        }

        //Generate random new Monster
        private static Monster GetMonster()
        {
            switch (RngThreadSafe.Next(1, 6))
            {
                case 1: return new Boar();
                    break;
                case 2: return new KoboldWarrior();
                    break;
                case 3: return new KoboldGatherer();
                    break;
                case 4: return new KoboldHunter();
                    break;
                default: return new Deer();
            }
        }
    }
}
