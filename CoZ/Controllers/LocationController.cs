using CoZ.Models;
using CoZ.Models.Items;
using CoZ.Models.Locations;
using CoZ.Repositories;
using CoZ.ViewModels;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoZ.Controllers
{
    [Authorize]
    public class LocationController : Controller
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

        public async Task<ActionResult> Index()
        {
            string id = User.Identity.GetUserId();
            var location = await this.LocationRepository.FindCurrentLocation(id);
            var monster = await this.MonsterRepository.FindMonsterByLocation(location);
            var modelView = await CreateViewModel(location);
            if (monster != null)
            {
                return RedirectToAction("Index", "Battle");
            }
            else return View(modelView);
        }

        public async Task<ActionResult> GoNorth()
        {
            string id = User.Identity.GetUserId();
            var character = await this.CharacterRepository.FindByCharacterId(id);
            if (character.YCoord == 20)
            {
                character.YCoord = 1;
            }
            else
            {
                character.YCoord += 1;
            }
            await this.CharacterRepository.UpdateCharacter(character);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> GoSouth()
        {
            string id = User.Identity.GetUserId();
            var character = await this.CharacterRepository.FindByCharacterId(id);
            if (character.YCoord == 1)
            {
                character.YCoord = 20;
            }
            else
            {
                character.YCoord -= 1;
            }
            await this.CharacterRepository.UpdateCharacter(character);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> GoEast()
        {
            string id = User.Identity.GetUserId();
            var character = await this.CharacterRepository.FindByCharacterId(id);
            if (character.XCoord == 20)
            {
                character.XCoord = 1;
            }
            else
            {
                character.XCoord += 1;
            }
            await this.CharacterRepository.UpdateCharacter(character);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> GoWest()
        {
            string id = User.Identity.GetUserId();
            var character = await this.CharacterRepository.FindByCharacterId(id);
            if (character.XCoord == 1)
            {
                character.XCoord = 20;
            }
            else
            {
                character.XCoord -= 1;
            }
            await this.CharacterRepository.UpdateCharacter(character);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Map()
        {
            var model = await CreateMapViewModel();
            return View(model);
        }

        public async Task<ActionResult> Camp()
        {
            var id = User.Identity.GetUserId();
            var character = await this.CharacterRepository.FindByCharacterId(id);
            character.Camp();
            await this.CharacterRepository.UpdateCharacter(character);
            return View();
        }

        public async Task<MapViewModel> CreateMapViewModel()
        {
            var id = User.Identity.GetUserId();
            var character = await this.CharacterRepository.FindByCharacterId(id);
            var map = await this.LocationRepository.GetMap(id);
            return new MapViewModel(map, character.XCoord, character.YCoord);
        }

        public async Task<LocationViewModel> CreateViewModel(Location location)
        {
            var id = User.Identity.GetUserId();
            var character = await this.CharacterRepository.FindByCharacterId(id);
            var isMonsterNorth  = CheckNorth(id, character);
            var isMonsterEast = CheckEast(id, character);
            var isMonsterSouth = CheckSouth(id, character);
            var isMonsterWest = CheckWest(id, character);
            var allBools = await Task.WhenAll(isMonsterNorth, isMonsterEast, isMonsterSouth, isMonsterWest).ConfigureAwait(false);
            LocationViewModel result = new LocationViewModel(location, isMonsterNorth.Result, isMonsterEast.Result, isMonsterSouth.Result, isMonsterWest.Result);
            return result;
        }

        public async Task<bool> CheckNorth(string id, Character character)
        {
            var x = character.XCoord;
            var y = 0;
            if (character.YCoord == 20)
            {
                y = 1;
            }
            else
            {
                y = character.YCoord + 1;
            }
            var location = await this.LocationRepository.FindLocation(id, x, y);
            var result = await this.LocationRepository.AreThereMonstersAtLocation(location);
            return result;
        }

        public async Task<bool> CheckSouth(string id, Character character)
        {
            var x = character.XCoord;
            var y = 0;
            if (character.YCoord == 1)
            {
                y = 20;
            }
            else
            {
                y = character.YCoord - 1;
            }
            var location = await this.LocationRepository.FindLocation(id, x, y);
            var result = await this.LocationRepository.AreThereMonstersAtLocation(location);
            return result;
        }

        public async Task<bool> CheckEast(string id, Character character)
        {
            var x = 0;
            var y = character.YCoord;
            if (character.XCoord == 20)
            {
                x = 1;
            }
            else
            {
                x = character.XCoord + 1;
            }
            var location = await this.LocationRepository.FindLocation(id, x, y);
            var result = await this.LocationRepository.AreThereMonstersAtLocation(location);
            return result;
        }

        public async Task<bool> CheckWest(string id, Character character)
        {
            var x = 0;
            var y = character.YCoord;
            if (character.XCoord == 1)
            {
                x = 20;
            }
            else
            {
                x = character.XCoord - 1;
            }
            var location = await this.LocationRepository.FindLocation(id, x, y);
            var result = await this.LocationRepository.AreThereMonstersAtLocation(location);
            return result;
        }

        public async Task<ActionResult> Market(bool? purchase = null)
        {
            if (purchase == true)
            {
                ViewBag.Message = "You have purchased the item.";
            }
            else if(purchase == false)
            {
                ViewBag.Message = "You do not have enough gold.";
            }
            return View();
        }

        public async Task<ActionResult> BuyPotion()
        {
            string id = User.Identity.GetUserId();
            var character = await this.CharacterRepository.FindByCharacterId(id);
            if (character.Gold < 5)
            {
                ViewBag.Message = "You do not have enough gold.";
            }
            else
            {
                character.Gold -= 5;
                await this.CharacterRepository.UpdateCharacter(character);
                await this.CharacterRepository.GainItem(id, new HealingPotion());
                ViewBag.Message = "You have purchased the item";
            }
            return View("Market");
        }
    }
}