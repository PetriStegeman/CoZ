using CoZ.Models;
using CoZ.Models.Items;
using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using CoZ.Repositories;
using CoZ.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoZ.Controllers
{
    [Authorize]
    public class BattleController : Controller
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
            var location = this.LocationRepository.FindCurrentLocation(id);
            var monster = this.MonsterRepository.FindMonsterByLocation(location);
            var result = CreateBattleViewModel(monster, character);
            if (monster == null)
            {
                return RedirectToAction("Index", "Location");
            }
            return View(result);
        }
        
        //Redirect the user to the Location view after defeating the monster(s)
        public ActionResult RunAWay()
        {
            string id = User.Identity.GetUserId();
            var location = this.LocationRepository.FindCurrentLocation(id);
            var monster = this.MonsterRepository.FindMonsterByLocation(location);
            this.MonsterRepository.DeleteMonster(monster);
            this.LocationRepository.UpdateLocation(location);
            return RedirectToAction("Index", "Location");
        }

        public ActionResult Attack()
        {
            string id = User.Identity.GetUserId();
            var character = this.CharacterRepository.FindByCharacterId(id);
            var location = this.LocationRepository.FindCurrentLocation(id);
            var monster = this.MonsterRepository.FindMonsterByLocation(location);
            character.Attack(monster);
            var result = CreateBattleViewModel(monster, character);
            this.CharacterRepository.UpdateCharacter(character);
            this.MonsterRepository.Updatemonster(monster);
            return DetermineBattleOutcome(result);
        }

        public ActionResult Victory()
        {
            string id = User.Identity.GetUserId();
            var character = this.CharacterRepository.FindByCharacterId(id);
            var location = this.LocationRepository.FindCurrentLocation(id);
            var monster = this.MonsterRepository.FindMonsterByLocation(location);
            var item = this.ItemRepository.FindLoot(monster);
            this.CharacterRepository.GainItem(id, item);
            character.Victory(monster);
            var result = CreateBattleViewModel(monster, character, item);
            this.MonsterRepository.DeleteMonster(monster);
            this.LocationRepository.UpdateLocation(location);
            this.CharacterRepository.UpdateCharacter(character);
            return View(result);
        }

        public ActionResult GameOver()
        {
            string id = User.Identity.GetUserId();
            this.CharacterRepository.DeleteCharacter(id);
            return View();
        }

        public ActionResult LevelUp()
        {
            bool levelUp = false;
            string id = User.Identity.GetUserId();
            var character = this.CharacterRepository.FindByCharacterId(id);
            var location = this.LocationRepository.FindCurrentLocation(id);
            levelUp = character.IsLevelUp();
            this.CharacterRepository.UpdateCharacter(character);
            if (levelUp)
            {
                return View();
            }
            else return RedirectToAction("Index", "Location");
        }

        public ActionResult DetermineBattleOutcome(BattleViewModel view)
        {
            if (view.CharacterCurrentHp <= 0)
            {
                return RedirectToAction("GameOver");
            }
            else if (view.MonsterCurrentHp <= 0)
            {
                return RedirectToAction("Victory");
            }
            else return RedirectToAction("Index");
        }

        public BattleViewModel CreateBattleViewModel(Monster monster, Character character, Item item = null)
        {
            return new BattleViewModel(monster, character, item);
        }

    }
}