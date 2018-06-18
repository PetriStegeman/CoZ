using CoZ.Models.Items;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Monsters
{
    public abstract class Monster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Strength { get; set; }
        public int Gold { get; set; }
        public virtual ICollection<Item> Loot { get; set; }

        //Random Loot and Random Gold assignment for new creature
        public void MonsterInit()
        {
            this.Loot = new Item[3];
            //ItemFactory.CreateItem(this);
            this.Gold = RngThreadSafe.Next(1, this.Level * 3);
        }
    }
}