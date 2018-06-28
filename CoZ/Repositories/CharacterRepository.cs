using CoZ.Models;
using CoZ.Models.Items;
using CoZ.Models.Locations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoZ.Repositories
{
    public class CharacterRepository 
    {
        public void CreateCharacter(string id, string name)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = new Character(id, name);
                dbContext.Characters.Add(character);
                dbContext.SaveChanges();
            }
        }

        public Character FindByCharacterId(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                return dbContext.Characters.SingleOrDefault(c => c.UserId == id);
            }
        }

        public void DeleteCharacter(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = dbContext.Characters.SingleOrDefault(c => c.UserId == id);
                DeleteLocations(dbContext, character);
                DeleteItems(dbContext, character.Inventory);
                dbContext.Characters.Remove(character);
                dbContext.SaveChanges();
            }
        }

        private void DeleteLocations(ApplicationDbContext dbContext, Character character)
        {
            IEnumerable<Location> map = character.Map;
            foreach (var location in map.ToList())
            {
                if (location.Monster != null)
                {
                    dbContext.Monsters.Remove(dbContext.Monsters.Find(location.Monster.MonsterId));
                }
                dbContext.Locations.Remove(dbContext.Locations.Find(location.LocationId));
            }
        }

        private void DeleteItems(ApplicationDbContext dbContext, ICollection<Item> items)
        {
            foreach (var item in items.ToList())
            {
                items.Remove(item);
                dbContext.Items.Remove(dbContext.Items.Find(item.ItemId));
            }
        }


        public void UpdateCharacter(Character character)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalCharacter = dbContext.Characters.Find(character.CharacterId);
                originalCharacter.CopyCharacter(character);
                dbContext.SaveChanges();
            }
        }

        public void GainItem(String id, Item item)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalItem = dbContext.Items.Find(item.ItemId);
                var newLoot = originalItem.CloneItem();
                var character = dbContext.Characters.SingleOrDefault(c => c.UserId == id);
                character.Inventory.Add(newLoot);
                dbContext.Items.Add(newLoot);
                dbContext.SaveChanges();
            }
        }
    }
}