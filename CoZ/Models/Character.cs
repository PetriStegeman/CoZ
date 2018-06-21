using CoZ.Models.Items;
using CoZ.Models.Locations;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public int Gold { get; set; }
        public virtual ICollection<Item> Inventory { get; set; }
        public virtual Map Map { get; set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        //Statistics
        public int Experience { get; set; }
        public int Level { get; set; }
        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }
        public int MaxMp { get; set; }
        public int CurrentMp { get; set; }
        public int Strength { get; set; }
        public int Magic { get; set; }
        public int Insanity { get; set; }

        public Location FindCurrentLocation()
        {
            return this.Map.WorldMap.Where(l => l.XCoord == this.XCoord && l.YCoord == this.YCoord).Single();
        }

        public void CopyCharacter(Character DesiredResult)
        {
            this.CharacterId = DesiredResult.CharacterId;
            this.UserId = DesiredResult.UserId;
            this.Name = DesiredResult.Name;
            this.Gold = DesiredResult.Gold;
            this.Inventory = DesiredResult.Inventory;
            this.Map = DesiredResult.Map;
            this.XCoord = DesiredResult.XCoord;
            this.YCoord = DesiredResult.YCoord;
            this.Experience = DesiredResult.Experience;
            this.Level = DesiredResult.Level;
            this.MaxHp = DesiredResult.MaxHp;
            this.CurrentHp = DesiredResult.CurrentHp;
            this.MaxMp = DesiredResult.MaxMp;
            this.CurrentMp = DesiredResult.CurrentMp;
            this.Strength = DesiredResult.Strength;
            this.Magic = DesiredResult.Magic;
            this.Insanity = DesiredResult.Insanity;
        }

        public Character(string id)
        {
            this.Map = MapFactory.CreateBigMap(id);
            this.UserId = id;
            this.XCoord = 10;
            this.YCoord = 10;
            this.CurrentHp = 10;
            this.MaxHp = 10;
            this.Strength = this.Level + 3;
        }

        public Character(){}
    }
}