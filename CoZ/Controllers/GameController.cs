using CoZ.Models;
using CoZ.Models.Locations;
using CoZ.Repositories;
using CoZ.Utility;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoZ.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
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

        // GET: Game
        public ActionResult Index()
        {
            return View();
        }

        //Generate data to start a new game
        public ActionResult Create()
        {
            string id = User.Identity.GetUserId();
            if (this.CharacterRepository.FindByCharacterId(id) != null)
            {
                this.CharacterRepository.DeleteCharacter(id);
            }
            this.CharacterRepository.CreateCharacter(id);
            this.MonsterRepository.AddMonsters(id);
            this.ItemRepository.AddItems(id);
            return RedirectToAction("Index", "Location");
        }
    }
}