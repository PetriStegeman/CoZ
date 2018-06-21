using CoZ.Models;
using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Repositories
{
    public class CharacterRepository //: Repository 
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
                return dbContext.Characters.Where(c => c.UserId == id).Single();
            }
        }

        //SOLID AND CLEAN IF YOU DONT AGREE FIGHT ME IRL
        public void DeleteCharacter(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = dbContext.Characters.Single(c => c.UserId == id);
                foreach (var location in character.Map)
                {
                    foreach (var monster in location.Monsters)
                    {
                        foreach (var item in monster.Loot)
                        {
                            dbContext.Items.Remove(dbContext.Items.Find(item.ItemId));
                        }
                        dbContext.Monsters.Remove(dbContext.Monsters.Find(monster.MonsterId));
                    }
                    dbContext.Locations.Remove(dbContext.Locations.Find(location.LocationId));
                }
                foreach (var item in character.Inventory)
                {
                    dbContext.Items.Remove(dbContext.Items.Find(item.ItemId));
                }
                dbContext.Characters.Remove(character);
                dbContext.SaveChanges();
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
    }
}