using CoZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.ViewModels
{
    public class LevelUpViewModel
    {
        public int Level { get; set; }
        public int Strength { get; set; }
        public int MaxHp { get; set; }
        public int MaxMp { get; set; }

        public LevelUpViewModel() { }

        public LevelUpViewModel(Character character)
        {
            this.Level = character.Level;
            this.Strength = character.Strength;
            this.MaxHp = character.MaxHp;
            this.MaxMp = character.MaxMp;
        }
    }
}