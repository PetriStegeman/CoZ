using CoZ.Models;
using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using CoZ.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoZ.Controllers
{
    [Authorize]
    public class LocationController : Controller
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
            var location = this.LocationRepository.FindCurrentLocation(id);
            var monster = this.MonsterRepository.FindMonsterByLocation(location);
            if (monster != null)
            {
                return RedirectToAction("Index", "Battle");
            }
            else return View(location);
        }

        public ActionResult GoNorth()
        {
            string id = User.Identity.GetUserId();
            var character = this.CharacterRepository.FindByCharacterId(id);
            if (character.YCoord == 20)
            {
                //TODO Throw out of bounds exception and handle it in javascript
            }
            character.YCoord += 1;
            var location = this.LocationRepository.FindCurrentLocation(id);
            this.CharacterRepository.UpdateCharacter(character);
            return RedirectToAction("Index", location);
        }

        public ActionResult GoSouth()
        {

            string id = User.Identity.GetUserId();
            var character = this.CharacterRepository.FindByCharacterId(id);
            if (character.YCoord == 0)
            {
                //TODO Throw out of bounds exception and handle it in javascript
            }
            character.YCoord -= 1;
            var location = this.LocationRepository.FindCurrentLocation(id);
            this.CharacterRepository.UpdateCharacter(character);
            return RedirectToAction("Index", location);
        }

        public ActionResult GoEast()
        {
            string id = User.Identity.GetUserId();
            var character = this.CharacterRepository.FindByCharacterId(id);
            if (character.XCoord == 20)
            {
                //TODO Throw out of bounds exception and handle it in javascript
            }
            character.XCoord += 1;
            var location = this.LocationRepository.FindCurrentLocation(id);
            this.CharacterRepository.UpdateCharacter(character);
            return RedirectToAction("Index", location);
        }

        public ActionResult GoWest()
        {
            string id = User.Identity.GetUserId();
            var character = this.CharacterRepository.FindByCharacterId(id);
            if (character.YCoord == 0)
            {
                //TODO Throw out of bounds exception and handle it in javascript
            }
            character.XCoord -= 1;
            var location = this.LocationRepository.FindCurrentLocation(id);
            this.CharacterRepository.UpdateCharacter(character);
            return RedirectToAction("Index", location);
        }

        public ActionResult Camp()
        {
            string id = User.Identity.GetUserId();
            var character = this.CharacterRepository.FindByCharacterId(id);
            character.Camp();
            this.CharacterRepository.UpdateCharacter(character);
            return View();
        }
    }
}