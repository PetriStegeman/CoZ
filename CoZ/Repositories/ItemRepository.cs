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
        public List<Item> GetInventory(string id)
        {
            List<Item> result = new List<Item>();
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = dbContext.Characters.Single(c => c.UserId == id);
                var inventory = character.Inventory.ToList();
                result = inventory;
            }
            return result;
        }

        /// <summary>
        /// Consume an item of type Consumable that has an effect on character with the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemName"></param>
        public void ConsumeItem(string id, string itemName)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = dbContext.Characters.Single(c => c.UserId == id);
                var inventory = character.Inventory.ToList();
                Potion item = (Potion) inventory.First(w => w.Name == itemName);
                if (item != null)
                {
                    item.Consume(character);
                    inventory.Remove(item);
                    dbContext.Items.Remove(dbContext.Items.Find(item.ItemId));
                }
                dbContext.SaveChanges();
            }
        }

        public void EquipItem(string id, string itemName)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = dbContext.Characters.Single(c => c.UserId == id);
                var inventory = character.Inventory.ToList();
                var item = inventory.First(w => w.Name == itemName);
                if (item is Armor)
                {
                    EquipArmor(id, item);
                }
                else if (item is Weapon)
                {
                    EquipWeapon(id, item);
                }
            }
        }

        private void EquipArmor(string id, Item item)
        {
            var equipedArmor = FindEquipedArmor(id);
            if (equipedArmor != null)
            {
                equipedArmor.IsEquiped = false;
                UpdateItem(equipedArmor);
            }
            item.IsEquiped = true;
            UpdateItem(item);
        }

        private void EquipWeapon(string id, Item item)
        {
            var equipedWeapon = FindEquipedWeapon(id);
            if (equipedWeapon != null)
            {
                equipedWeapon.IsEquiped = false;
                UpdateItem(equipedWeapon);
            }
            item.IsEquiped = true;
            UpdateItem(item);
        }

        public Item FindLoot(Monster monster)
        {
            Item result = null;
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalMonster = dbContext.Monsters.Find(monster.MonsterId);
                if (originalMonster.Loot != null)
                {
                    var item = dbContext.Items.Find(originalMonster.Loot.ItemId);
                    result = item.CloneItem();
                }
            }
            return result;
        }

        public Item FindEquipedWeapon(string id)
        {
            Item result = null;
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = dbContext.Characters.Single(c => c.UserId == id);
                var weapon = character.Inventory.SingleOrDefault(w => w.IsEquiped == true && w is Weapon);
                if (weapon != null)
                {
                    result = weapon.CloneItem();
                }
            }
            return result;
        }

        public Item FindEquipedArmor(string id)
        {
            Item result = null;
            using (var dbContext = ApplicationDbContext.Create())
            {
                var character = dbContext.Characters.Single(c => c.UserId == id);
                var armor = character.Inventory.SingleOrDefault(a => a.IsEquiped == true && a is Armor);
                if (armor != null)
                {
                    result = armor.CloneItem();
                }
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

        public void UpdateItem(Item item)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {
                var originalItem = dbContext.Items.Find(item.ItemId);
                originalItem.CopyItem(item);
                dbContext.SaveChanges();
            }
        }
    }
}