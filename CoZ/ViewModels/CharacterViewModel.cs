using CoZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.ViewModels
{
    public class CharacterViewModel
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int CurrentHp { get; set; }
        public int MaxHp { get; set; }
        public int CurrentMp { get; set; }
        public int MaxMp { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        public int Gold { get; set; }
        public string WeaponName { get; set; }
        public string ArmorName { get; set; }

        public CharacterViewModel() { }

        public CharacterViewModel(Character character, string WeaponName, string ArmorName)
        {
            this.Name = character.Name;
            this.Level = character.Level;
            this.CurrentHp = character.CurrentHp;
            this.MaxHp = character.MaxHp;
            this.CurrentMp = character.CurrentMp;
            this.MaxMp = character.MaxMp;
            this.Speed = character.Speed;
            this.Strength = character.Strength;
            this.Gold = character.Gold;
            this.WeaponName = WeaponName;
            this.ArmorName = ArmorName;
        }
    }
}