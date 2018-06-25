using CoZ.Models;
using CoZ.Models.Items;
using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Repositories
{
    public class CharacterRepository 
    {
        public void CreateCharacter(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = new Character(id);
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
                foreach (var item in character.Inventory)
                {
                    dbContext.Items.Remove(dbContext.Items.Find(item.ItemId));
                }
                dbContext.Characters.Remove(character);
                dbContext.SaveChanges();
            }
        }

        private static void DeleteLocations(ApplicationDbContext dbContext, Character character)
        {
            IEnumerable<Location> locationsWithMonstersToDelete = character.Map.Where(d => d.Monster != null);
            foreach (var location in locationsWithMonstersToDelete)
            {
                dbContext.Items.Remove(dbContext.Items.Find(location.Monster.Loot.ItemId));
                dbContext.Monsters.Remove(dbContext.Monsters.Find(location.Monster.MonsterId));
            }
            IEnumerable<Location> locationsWithItemsToDelete = character.Map.Where(d => d.Item != null);
            foreach (var location in locationsWithItemsToDelete)
            {
                dbContext.Items.Remove(dbContext.Items.Find(location.Item.ItemId));
            }
            foreach (var location in character.Map)
            {
                dbContext.Locations.Remove(dbContext.Locations.Find(location.LocationId));
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
                var character = new Character(id);
                var newLoot = item.CloneItem();
                character.Inventory.Add(newLoot);
                dbContext.Items.Add(newLoot);
                dbContext.SaveChanges();
            }
        }
    }
}