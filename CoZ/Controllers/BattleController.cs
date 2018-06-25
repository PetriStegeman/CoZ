using CoZ.Models;
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

        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            var character = this.CharacterRepository.FindByCharacterId(id);
            var location = this.LocationRepository.FindCurrentLocation(id);
            var monster = this.MonsterRepository.FindMonsterByLocation(location);
            if (monster == null)
            {
                return RedirectToAction("Index", "Location");
            }
            return View(monster);
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

        //PartialView implementeren?
        public ActionResult Attack()
        {
            string id = User.Identity.GetUserId();
            var character = this.CharacterRepository.FindByCharacterId(id);
            var location = this.LocationRepository.FindCurrentLocation(id);
            var monster = this.MonsterRepository.FindMonsterByLocation(location);
            character.Attack(monster);
            var result = SetBattleViewModel(monster, character);
            this.CharacterRepository.UpdateCharacter(character);
            this.MonsterRepository.Updatemonster(monster);
            return DetermineBattleOutcome(result);
        }

        public ActionResult Victory()
        {
            //VictoryViewModel result = null;
            Monster result = null; //TODO Replace
            string id = User.Identity.GetUserId();
            var character = this.CharacterRepository.FindByCharacterId(id);
            var location = this.LocationRepository.FindCurrentLocation(id);
            var monster = this.MonsterRepository.FindMonsterByLocation(location);
            character.Victory(monster);
            //result = SetVictoryViewModel(monster);
            result.CopyMonster(monster); //TODO Replace
            this.MonsterRepository.DeleteMonster(monster);
            this.LocationRepository.UpdateLocation(location);
            this.CharacterRepository.UpdateCharacter(character);
            return View(result);
        }

        public ActionResult GameOver()
        {
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

        public BattleViewModel SetBattleViewModel(Monster monster, Character character)
        {
            return new BattleViewModel(monster, character);
        }

        public VictoryViewModel SetVictoryViewModel(Monster monster)
        {
            return new VictoryViewModel(monster);
        }

        public LevelUpViewModel SetLevelUpViewModel(Character character)
        {
            return new LevelUpViewModel(character);
        }


    }
}