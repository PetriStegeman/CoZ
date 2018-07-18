using CoZ.Models;
using CoZ.Models.Items;
using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CoZ.Repositories
{
    public class ItemRepository
    {
        public async Task<List<Item>> GetInventory(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = await Task.Run(() => dbContext.Characters.Single(c => c.UserId == id));
                var inventory = character.Inventory.ToList();
                return inventory;
            }
        }

        /// <summary>
        /// Consume an item of type Consumable that has an effect on character with the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemName"></param>
        public async Task ConsumeItem(string id, string itemName)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = await Task.Run(() => dbContext.Characters.Single(c => c.UserId == id));
                var inventory = character.Inventory.ToList();
                Potion item = await Task.Run(() => (Potion) inventory.FirstOrDefault(w => w.Name == itemName));
                if (item != null)
                {
                    item.Consume(character);
                    inventory.Remove(item);
                    await Task.Run(() => dbContext.Items.Remove(dbContext.Items.Find(item.ItemId)));
                }
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task EquipItem(string id, string itemName)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = await Task.Run(() => dbContext.Characters.Single(c => c.UserId == id));
                var inventory = character.Inventory.ToList();
                var item = inventory.First(w => w.Name == itemName);
                if (item is Armor)
                {
                    await EquipArmor(id, item);
                }
                else if (item is Weapon)
                {
                    await EquipWeapon(id, item);
                }
            }
        }

        private async Task EquipArmor(string id, Item item)
        {
            var equipedArmor = await FindEquipedArmor(id);
            if (equipedArmor != null)
            {
                equipedArmor.IsEquiped = false;
                await UpdateItem(equipedArmor);
            }
            item.IsEquiped = true;
            await UpdateItem(item);
        }

        private async Task EquipWeapon(string id, Item item)
        {
            var equipedWeapon = await FindEquipedWeapon(id);
            if (equipedWeapon != null)
            {
                equipedWeapon.IsEquiped = false;
                await UpdateItem(equipedWeapon);
            }
            item.IsEquiped = true;
            await UpdateItem(item);
        }

        public async Task<Item> FindLoot(Monster monster)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalMonster = await Task.Run(() => dbContext.Monsters.Find(monster.MonsterId));
                if (originalMonster.Loot != null)
                {
                    var item = await Task.Run(() => dbContext.Items.Find(originalMonster.Loot.ItemId));
                    return item.CloneItem();
                }
                else return null;
            }
        }

        public async Task<Item> FindEquipedWeapon(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = await Task.Run(() => dbContext.Characters.Single(c => c.UserId == id));
                var weapon = await Task.Run(() => character.Inventory.SingleOrDefault(w => w.IsEquiped == true && w is Weapon));
                if (weapon != null)
                {
                    return weapon.CloneItem();
                }
                else return null;
            }
        }

        public async Task<Item> FindEquipedArmor(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = await Task.Run(() => dbContext.Characters.Single(c => c.UserId == id));
                var armor =  await Task.Run(() => character.Inventory.SingleOrDefault(a => a.IsEquiped == true && a is Armor));
                if (armor != null)
                {
                    return armor.CloneItem();
                }
                else return null;
            }
        }

        public async Task AddItems(string id)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = await Task.Run(() => dbContext.Characters.Single(c => c.UserId == id));
                ICollection<Location> map = character.Map;
                foreach (var location in map)
                {
                    var item = ItemFactory.CreateItem();
                    if (location.Monster == null && item != null)
                    {
                        await Task.Run(() => dbContext.Items.Add(item));
                        location.Item = item;
                    }
                }
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateItem(Item item)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalItem = await Task.Run(() => dbContext.Items.Find(item.ItemId));
                originalItem.CopyItem(item);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}