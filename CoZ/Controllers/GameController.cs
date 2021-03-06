﻿using CoZ.Models;
using CoZ.Models.Locations;
using CoZ.Repositories;
using CoZ.Utility;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CoZ.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        #region Repositories
        #pragma warning disable
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
        #pragma warning restore
        #endregion

        // GET: Game
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Continue()
        {
            string id = User.Identity.GetUserId();
            if (await this.CharacterRepository.FindByCharacterId(id) == null)
            {
                ViewBag.Message = "You cannot continue the game, please create a new Character.";
                return View("Index");
            }
            else return RedirectToAction("Index");
        }

        //Generate data to start a new game
        public async Task<ActionResult> Initialize(string name)
        {
            string id = User.Identity.GetUserId();
            if (await this.CharacterRepository.FindByCharacterId(id) != null)
            {
                await this.CharacterRepository.DeleteCharacter(id);
            }
            await this.CharacterRepository.CreateCharacter(id, name);
            var addMonsters = this.MonsterRepository.AddMonsters(id);
            var addItems = this.ItemRepository.AddItems(id);
            await Task.WhenAll(addMonsters, addItems).ConfigureAwait(false);
            return RedirectToAction("Index", "Location");
        }

        public ActionResult Create()
        {
            return View();
        }

    }
}