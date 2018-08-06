using CoZ.Models.Items;
using CoZ.Repositories;
using CoZ.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CoZ.Controllers
{
    public class InventoryController : Controller
    {
        #region Repositories
        private LocationRepository locationRepository;
        protected LocationRepository LocationRepository
        {
            get
            {
                if (locationRepository == null)
                {
                    return new LocationRepository();
                }
                else
                {
                    return locationRepository;
                }
            }
        }

        private CharacterRepository characterRepository;
        protected CharacterRepository CharacterRepository
        {
            get
            {
                if (characterRepository == null)
                {
                    return new CharacterRepository();
                }
                else
                {
                    return characterRepository;
                }
            }
        }

        private MonsterRepository monsterRepository;
        protected MonsterRepository MonsterRepository
        {
            get
            {
                if (monsterRepository == null)
                {
                    return new MonsterRepository();
                }
                else
                {
                    return monsterRepository;
                }
            }
        }

        private ItemRepository itemRepository;
        protected ItemRepository ItemRepository
        {
            get
            {
                if (itemRepository == null)
                {
                    return new ItemRepository();
                }
                else
                {
                    return itemRepository;
                }
            }
        }
        #endregion

        public async Task<ActionResult> Index()
        {
            string id = User.Identity.GetUserId();
            var inventory = await this.ItemRepository.GetInventory(id);
            var viewModel = new InventoryViewModel(inventory);
            return View(viewModel);
        }

        public async Task<ActionResult> ConsumeItem(string itemName = "Healing Potion")
        {
            string id = User.Identity.GetUserId();
            await this.ItemRepository.ConsumeItem(id, itemName);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> EquipItem(string itemName)
        {
            string id = User.Identity.GetUserId();
            await this.ItemRepository.EquipItem(id, itemName);
            return RedirectToAction("Index");
        }
    }
}