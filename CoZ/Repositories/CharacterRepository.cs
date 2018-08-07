using CoZ.Models;
using CoZ.Models.Items;
using CoZ.Models.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoZ.Repositories
{
    public class CharacterRepository 
    {
        public async Task CreateCharacter(string id, string name)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = new Character(id, name);
                await Task.Run(() => dbContext.Characters.Add(character));
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<Character> FindByCharacterId(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                return await Task.Run(() => dbContext.Characters.SingleOrDefault(c => c.UserId == id));
            }
        }

        public async Task DeleteCharacter(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = await Task.Run(() => dbContext.Characters.SingleOrDefault(c => c.UserId == id));
                await DeleteLocations(dbContext, character);
                await DeleteItems(dbContext, character.Inventory);
                await Task.Run(() => dbContext.Characters.Remove(character));
                await dbContext.SaveChangesAsync();
            }
        }

        private async Task DeleteLocations(ApplicationDbContext dbContext, Character character)
        {
            IEnumerable<Location> map = character.Map;
            foreach (var location in map.ToList())
            {
                if (location.Monster != null)
                {
                    await Task.Run(() => dbContext.Monsters.Remove(dbContext.Monsters.Find(location.Monster.MonsterId)));
                }
                await Task.Run(() => dbContext.Locations.Remove(dbContext.Locations.Find(location.LocationId)));
            }
        }

        private async Task DeleteItems(ApplicationDbContext dbContext, ICollection<Item> items)
        {
            foreach (var item in items.ToList())
            {
                await Task.Run(() => items.Remove(item));
                await Task.Run(() => dbContext.Items.Remove(dbContext.Items.Find(item.ItemId)));
            }
        }


        public async Task UpdateCharacter(Character character)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalCharacter = await Task.Run(() => dbContext.Characters.Find(character.CharacterId));
                await Task.Run(() => originalCharacter.CopyCharacter(character));
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task GainItem(String id, Item item)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = await Task.Run(() => dbContext.Characters.SingleOrDefault(c => c.UserId == id));
                await Task.Run(() => character.Inventory.Add(item));
                await Task.Run(() => dbContext.Items.Add(item));
                await dbContext.SaveChangesAsync();
            }
        }
    }
}