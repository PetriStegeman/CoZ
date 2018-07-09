using CoZ.Models.Locations;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Monsters
{
    public class KoboldHunter : Monster
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

        public KoboldHunter(Location location)
        {
            this.Name = "Kobold Hunter";
            this.Level = RngThreadSafe.Next(3, 5);
            this.MaxHp = 4 + this.Level;
            this.CurrentHp = this.MaxHp;
            this.Strength = this.Level - 1;
            this.Speed = 5;
            this.MonsterInit();
            this.Location = location;
        }

        public KoboldHunter()
        {
            this.Name = "Kobold Hunter";
            this.Level = RngThreadSafe.Next(3, 5);
            this.MaxHp = 4 + this.Level;
            this.CurrentHp = this.MaxHp;
            this.Strength = this.Level - 1;
            this.Speed = 5;
            this.MonsterInit();
        }

    }
}