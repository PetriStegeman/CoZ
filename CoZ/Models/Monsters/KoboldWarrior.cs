using CoZ.Models.Locations;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Monsters
{
    public class KoboldWarrior : Monster
    {
        public override Monster CloneMonster()
        {
            var output = new KoboldWarrior();
            output.MonsterId = this.MonsterId;
            output.Name = this.Name;
            output.Level = this.Level;
            output.CurrentHp = this.CurrentHp;
            output.MaxHp = this.MaxHp;
            output.Strength = this.Strength;
            output.Gold = this.Gold;
            return output;
        }

        public KoboldWarrior(Location location)
        {
            this.Name = "Kobold Warrior";
            this.Level = RngThreadSafe.Next(3, 6);
            this.MaxHp = 10 + this.Level;
            this.CurrentHp = this.MaxHp;
            this.Strength = 1 + this.Level;
            this.Speed = 3;
            this.MonsterInit();
            this.Location = location;
        }

        public KoboldWarrior()
        {
            this.Name = "Kobold Warrior";
            this.Level = RngThreadSafe.Next(3,6);
            this.MaxHp = 10 + this.Level;
            this.CurrentHp = this.MaxHp;
            this.Strength = 1 + this.Level;
            this.Speed = 2;
            this.MonsterInit();
        }
    }
}