using CoZ.Models.Locations;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models.Monsters
{
    public class Boar : Monster
    {
        public override Monster CloneMonster()
        {
            var output = new Boar();
            output.MonsterId = this.MonsterId;
            output.Name = this.Name;
            output.Level = this.Level;
            output.CurrentHp = this.CurrentHp;
            output.MaxHp = this.MaxHp;
            output.Strength = this.Strength;
            output.Gold = this.Gold;
            return output;
        }

        public Boar(Location location)
        {
            this.Name = "Boar";
            this.Level = RngThreadSafe.Next(1, 4);
            this.MaxHp = 6 + this.Level;
            this.CurrentHp = this.MaxHp;
            this.Strength = this.Level;
            this.Speed = this.Level;
            this.Location = location;
        }

        public Boar()
        {
            this.Name = "Boar";
            this.Level = RngThreadSafe.Next(1, 4);
            this.MaxHp = 6 + this.Level;
            this.CurrentHp = this.MaxHp;
            this.Strength = this.Level;
            this.Speed = this.Level;
        }
    }
}