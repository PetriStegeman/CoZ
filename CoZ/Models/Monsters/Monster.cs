using CoZ.Models.Items;
using CoZ.Models.Locations;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoZ.Models.Monsters
{
    public abstract class Monster
    {
        [ForeignKey("Location")]
        public int MonsterId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int CurrentHp { get; set; }
        public int MaxHp { get; set; }
        public int Strength { get; set; }
        public int Gold { get; set; }
        public int Speed { get; set; }
        public virtual Item Loot { get; set; }
        public virtual Location Location { get; set; }

        public abstract Monster CloneMonster();

        public virtual void CopyMonster(Monster desiredResult)
        {
            this.MonsterId = desiredResult.MonsterId;
            this.Name = desiredResult.Name;
            this.Level = desiredResult.Level;
            this.CurrentHp = desiredResult.CurrentHp;
            this.MaxHp = desiredResult.MaxHp;
            this.Strength = desiredResult.Strength;
            this.Gold = desiredResult.Gold;
            this.Speed = desiredResult.Speed;
        }

        //Random Loot and Random Gold assignment for new creature
        public void MonsterInit()
        {
            //this.Loot = new List<Item>();
            //ItemFactory.CreateItem(this);
            this.Gold = RngThreadSafe.Next(1, this.Level * 3);
        }
    }
}