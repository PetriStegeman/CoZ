using CoZ.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Monsters
{
    public abstract class Monster
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Strength { get; set; }
        public int Gold { get; set; }
        public Item[] Loot { get; set; }

        //Random Loot and Random Gold assignment for new creature
        public void MonsterInit()
        {
            this.Loot = new Item[3];
            //ItemFactory.CreateItem(this);
            Random rndm = new Random();
            this.Gold = rndm.Next(1, this.Level * 3);
        }
    }
}