using CoZ.Models.Items;
using CoZ.Models.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.ViewModels
{
    public class VictoryViewModel
    {
        public int Gold { get; set; }
        public int Experience { get; set; }
        public Item Loot { get; set; }

        public VictoryViewModel() { }

        public VictoryViewModel(Monster monster)
        {
            this.Gold = monster.Gold;
            this.Experience = monster.Level;
            this.Loot = monster.Loot;
        }
    }
}