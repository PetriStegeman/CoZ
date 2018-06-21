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

        public ActionResult Index()
        {
            Location location = null;
            int monsterCounter = 0;
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                var character = this.CharacterRepository.FindByCharacterId(id);
                location = character.FindCurrentLocation();
                monsterCounter = location.Monsters.Count();
            }
            return BattleRedirect(location, monsterCounter);
        }

        private ActionResult BattleRedirect(Location location, int monsterCounter)
        {
            if (monsterCounter != 0)
            {
                return RedirectToAction("Index", "Battle");
            }
            else return View("Index", location);
        }

        public ActionResult GoNorth()
        {
            Location result = null;
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                var character = this.CharacterRepository.FindByCharacterId(id);
                character.YCoord += 1;
                var location = character.FindCurrentLocation();
                result.CopyLocation(location);
                this.CharacterRepository.UpdateCharacter(character);
            }
            return RedirectToAction("Index", result);
        }

        public ActionResult GoSouth()
        {
            Location result = null;
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                var character = this.CharacterRepository.FindByCharacterId(id);
                character.YCoord -= 1;
                var location = character.FindCurrentLocation();
                result.CopyLocation(location);
                this.CharacterRepository.UpdateCharacter(character);
            }
            return RedirectToAction("Index", result);
        }

        public ActionResult GoEast()
        {
            Location result = null;
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                var character = this.CharacterRepository.FindByCharacterId(id);
                character.XCoord += 1;
                var location = character.FindCurrentLocation();
                result.CopyLocation(location);
                this.CharacterRepository.UpdateCharacter(character);
            }
            return RedirectToAction("Index", result);
        }

        public ActionResult GoWest()
        {
            Location result = null;
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                var character = this.CharacterRepository.FindByCharacterId(id);
                character.XCoord -= 1;
                var location = character.FindCurrentLocation();
                result.CopyLocation(location);
                this.CharacterRepository.UpdateCharacter(character);
            }
            return RedirectToAction("Index", result);
        }

        public ActionResult Camp()
        {
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                var character = this.CharacterRepository.FindByCharacterId(id);
                character.Camp();
                this.CharacterRepository.UpdateCharacter(character);
            }
            return View();
        }
    }
}