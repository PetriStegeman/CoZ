using CoZ.Models;
using CoZ.Models.Items;
using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Repositories
{
    public class ItemRepository
    {

        public Item FindLoot(Monster monster)
        {
            Item result;
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalMonster = dbContext.Monsters.Find(monster.MonsterId);
                var item = dbContext.Items.Find(originalMonster.Loot.ItemId);
                result = item.CloneItem();
            }
            return result;
        }

        internal void AddItems(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = dbContext.Characters.Single(c => c.UserId == id);
                ICollection<Location> map = character.Map;
                foreach (var location in map)
                {
                    var item = ItemFactory.CreateItem();
                    if (location.Monster == null && item != null)
                    {
                        dbContext.Items.Add(item);
                        location.Item = item;
                    }
                }
                dbContext.SaveChanges();
            }
        }
    }
}