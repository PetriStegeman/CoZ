using CoZ.Models.Locations;

namespace CoZ.Models.Monsters
{
    public class Deer : Monster
    {
        public override Monster CloneMonster()
        {
            var output = new Deer();
            output.MonsterId = this.MonsterId;
            output.Name = this.Name;
            output.Level = this.Level;
            output.CurrentHp = this.CurrentHp;
            output.MaxHp = this.MaxHp;
            output.Strength = this.Strength;
            output.Gold = this.Gold;
            return output;
        }

        public Deer(Location location)
        {
            this.Name = "Deer";
            this.Level = 1;
            this.MaxHp = 4;
            this.CurrentHp = this.MaxHp;
            this.Strength = 1;
            this.Speed = 1;
            this.Location = location;
        }

        public Deer()
        {
            this.Name = "Deer";
            this.Level = 1;
            this.MaxHp = 4;
            this.CurrentHp = this.MaxHp;
            this.Strength = 1;
            this.Speed = 1;
        }
    }
}