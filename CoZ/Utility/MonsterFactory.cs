/*
using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Utility
{
    public class MonsterFactory
    {
        //20% chance to add Monster to Monsterlist of Location
        public static void CreateMonster(Location location)
        {
            if (RngThreadSafe.Next(1, 100) <= 20)
            {
                location.Monsters.Add(GetMonster());
            }
        }

        //Generate random new Monster
        private static Monster GetMonster()
        {
            //Temporary token Monster, to be replaced
            Monster result = new Boar();
            //TODO Return a random monster from our monster database
            return result;
        }
    }
}
*/