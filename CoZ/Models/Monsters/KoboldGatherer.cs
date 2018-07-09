using CoZ.Models.Locations;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Monsters
{
    public class KoboldGatherer : Monster
    {
        public override Monster CloneMonster()
        {
            var output = new KoboldGatherer();
            output.MonsterId = this.MonsterId;
            output.Name = this.Name;
            output.Level = this.Level;
            output.CurrentHp = this.CurrentHp;
            output.MaxHp = this.MaxHp;
            output.Strength = this.Strength;
            output.Gold = this.Gold;
            return output;
        }

        public KoboldGatherer(Location location)
        {
            this.Name = "Kobold Gatherer";
            this.Level = RngThreadSafe.Next(2, 4);
            this.MaxHp = 4 + this.Level;
            this.CurrentHp = this.MaxHp;
            this.Strength = this.Level;
            this.Speed = 3;
            this.MonsterInit();
            this.Location = location;
        }

        public KoboldGatherer()
        {
            this.Name = "Kobold Gatherer";
            this.Level = RngThreadSafe.Next(2, 4);
            this.MaxHp = 4 + this.Level;
            this.CurrentHp = this.MaxHp;
            this.Strength = this.Level;
            this.Speed = 3;
            this.MonsterInit();
        }
    }
}