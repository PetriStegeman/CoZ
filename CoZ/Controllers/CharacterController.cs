using CoZ.Models;
using CoZ.Models.Items;
using CoZ.Repositories;
using CoZ.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoZ.Controllers
{
    public class CharacterController : Controller
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

        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            var character = this.CharacterRepository.FindByCharacterId(id);
            var weapon = this.ItemRepository.FindEquipedWeapon(id);
            var armor = this.ItemRepository.FindEquipedArmor(id);
            var result = CreateViewModel(character, weapon, armor);
            return View(result);
        }

        private CharacterViewModel CreateViewModel(Character character, Item weapon, Item armor)
        {
            var weaponName = "";
            var armorName = "";
            if (weapon == null)
            {
                weaponName = "There is no weapon equiped.";
            }
            else
            {
                weaponName = weapon.Description;
            }
            if (armor == null)
            {
                armorName = "There is no armor equiped.";
            }
            else
            {
                armorName = armor.Description;
            }
            return new CharacterViewModel(character, weaponName, armorName);
        }
    }
}