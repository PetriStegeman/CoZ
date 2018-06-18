using CoZ.Models;
using CoZ.Models.Monsters;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoZ.Controllers
{
    public class BattleController : Controller
    {
        // GET: Battle
        public ActionResult Index()
        {
            Monster result = null;
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                Character myChar = DbContext.Characters.Where(c => c.userId == id).First();
                Monster monster = myChar.CurrentLocation.Monsters.First();
                result = MonsterCopy(monster);
            }
            return View(result);
        }
        
        //Redirect the user to the Location view after defeating the monster(s)
        public ActionResult ReDirect()
        {
            //TODO Get current location from database
            //TODO Remove monster(s) from current location list
            return RedirectToAction("Index", "Location");
        }

        //PartialView implementeren?
        public ActionResult Attack()
        {
            int monsterHp = 0;
            int characterHp = 0;
            using (var DbContext = ApplicationDbContext.Create())
            {
                Character myChar = DbContext.Characters.Find();
                Monster monster = myChar.CurrentLocation.Monsters.First();
                myChar.CurrentHp -= monster.Strength;
                monster.Hp -= myChar.Strength;
                monsterHp = monster.Hp;
                characterHp = myChar.CurrentHp;
                DbContext.SaveChanges();
            }
            if (characterHp == 0)
            {
                return RedirectToAction(""); //TODO Game over screen
            }
            else if (monsterHp == 0)
            {
                return View(""); //TODO Loot Screen? Terug naar location? Add daar logic om monster uit lijst te verwijderen
            }
            else return View("Index");
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