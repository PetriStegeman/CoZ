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

        // GET: Game
        public ActionResult Index()
        {
            return View();
        }

        //Generate data to start a new game
        public ActionResult Create()
        {
            string id = User.Identity.GetUserId();
            //this.CharacterRepository.DeleteExistingCharacters(id);
            this.CharacterRepository.CreateCharacter(id);
            return RedirectToAction("Index", "Location");
        }
    }
}