using CoZ.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.ViewModels
{
    public class InventoryViewModel
    {
            public List<InventoryItem> Inventory { get; set; }
            public int InventoryId { get; set; }

            public InventoryViewModel()
            {
                this.Inventory = new List<InventoryItem>();
            }

            public InventoryViewModel(List<Item> items)
            {
                List<InventoryItem> newInventory = new List<InventoryItem>();
                foreach (Item item in items)
                {
                    var inventoryItem = new InventoryItem(item);
                    newInventory.Add(inventoryItem);
                }
                this.Inventory = newInventory;
            }
    }

    public class InventoryItem
    {
        public int InventoryItemId { get; set; }
        public string ItemName { get; set; }
        public EItemType ItemType { get; set; }
        public int Value { get; set; }
        public string ItemDescription { get; set; }

        public InventoryItem() { }

        public InventoryItem(Item item)
        {
            this.ItemName = item.Name;
            this.Value = item.Value;
            this.ItemType = item.ItemType;
            this.ItemDescription = item.Description;
        }
    }
}