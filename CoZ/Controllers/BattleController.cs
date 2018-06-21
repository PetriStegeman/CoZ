using CoZ.Models;
using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using CoZ.Repositories;
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
        // GET: Battle
        public ActionResult Index()
        {
            Monster result = null;
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                Location location = LocationRepository.FindByCharacterId(id);
                if (DbContext.Monsters.Where(c => c.Location.LocationId == location.LocationId).Count() != 0)
                {
                    Monster monster = DbContext.Monsters.Where(c => c.Location.LocationId == location.LocationId).First();
                    result = MonsterCopy(monster);
                }
            }
            if (result == null)
            {
                return RedirectToAction("Index", "Location");
            }
            return View(result);
        }
        
        //Redirect the user to the Location view after defeating the monster(s)
        public ActionResult RunAWay()
        {
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                Location location = LocationRepository.FindByCharacterId(id);
                location.Monsters.Clear();
                DbContext.SaveChanges();
            }
            return RedirectToAction("Index", "Location");
        }

        //PartialView implementeren?
        public ActionResult Attack()
        {
            int monsterHp = 0;
            int characterHp = 0;
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                Character myChar = DbContext.Characters.Where(c => c.UserId == id).First();
                Location location = LocationRepository.FindByCharacterId(id);
                Monster monster = DbContext.Monsters.Where(c => c.Location.LocationId == location.LocationId).First();
                myChar.CurrentHp -= monster.Strength;
                monster.Hp -= myChar.Strength;
                monsterHp = monster.Hp;
                characterHp = myChar.CurrentHp;
                DbContext.SaveChanges();
            }
            if (characterHp <= 0)
            {
                return View("GameOver"); 
            }
            else if (monsterHp <= 0)
            {
                return RedirectToAction("Victory");
            }
            else return RedirectToAction("Index");
        }

        public ActionResult Victory()
        {
            Monster result = null;
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                Character myChar = DbContext.Characters.Where(c => c.UserId == id).First();
                Location location = LocationRepository.FindByCharacterId(id);
                Monster monster = DbContext.Monsters.Where(c => c.Location.LocationId == location.LocationId).First();
                myChar.Gold += monster.Gold;
                myChar.Experience += monster.Level;
                result = MonsterCopy(monster);
                location.Monsters.Clear();
                DbContext.SaveChanges();
            }
            return View(result);
        }

        public ActionResult GameOver()
        {
            return View();
        }

        public ActionResult LevelUp()
        {
            bool levelUp = false;
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                Character myChar = DbContext.Characters.Where(c => c.UserId == id).First();
                Location currentLocation = DbContext.Locations.Where(l => l.XCoord == myChar.XCoord && l.YCoord == myChar.YCoord).First();
                if (myChar.Experience > (myChar.Level * 5))
                {
                    levelUp = true;
                    myChar.Experience = 0;
                    myChar.Level += 1;
                    myChar.MaxHp += 1;
                    myChar.Strength += 1;
                }
                DbContext.SaveChanges();
            }
            if (levelUp == true)
            {
                return View();
            }
            else return RedirectToAction("Index", "Location");
        }

        //HELPER METHODES
        //TO DO FIX A REGION AT SOME POINT
        public Monster MonsterCopy(Monster input)
        {
            Monster output = new Boar(); //Misschien toch zoals het hoort Monster class gebruiken voor alle monsters?
            output.Hp = input.Hp;
            output.Gold = input.Gold;
            output.Level = input.Level;
            output.Loot = input.Loot;
            output.Name = input.Name;
            output.Strength = input.Strength;
            return output;
        }
    }
}