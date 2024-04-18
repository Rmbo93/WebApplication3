using Microsoft.AspNetCore.Mvc;
using static WebApplication3.Models.EmojiGameModel;
namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AnimalModel _animalModel;
        private readonly FoodModel _foodModel;
        private readonly FruitModel _fruitModel;
        private static List<UserModel> _userModel = new List<UserModel>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _animalModel = new AnimalModel();
            _foodModel = new FoodModel();
            _fruitModel = new FruitModel();
        }

        public IActionResult Index()
        {
            TempData.Keep("MatchResult");
            TempData.Keep("GameState");
            ViewBag.MatchResult = TempData["MatchResult"] as string;
            ViewBag.GameState = TempData["GameState"] as string ?? "NotStarted";

            // Include the user list when returning the view
            return View((Shuffle(_animalModel.AnimalEmoji), Shuffle(_foodModel.FoodEmoji), Shuffle(_fruitModel.FruitEmoji), _userModel));
        }

        [HttpPost]
        public IActionResult Index(string selectedEmoji1, string selectedEmoji2)
        {
            if (!string.IsNullOrEmpty(selectedEmoji1))
            {
                TempData["GameState"] = "Running";
                var user = _userModel.FirstOrDefault(u => u.Username.Equals(TempData["CurrentPlayer"]));
                if (user != null)
                {
                    user.GamesPlayed++;
                }
            }

            if (!string.IsNullOrEmpty(selectedEmoji1) && selectedEmoji1 == selectedEmoji2)
            {
                RemoveEmoji(selectedEmoji1);
                TempData["MatchResult"] = "It's a match!";
                TempData["MatchCount"] = (TempData["MatchCount"] as int? ?? 0) + 1;

                if (_animalModel.AnimalEmoji.Count == 0 &&
                    _foodModel.FoodEmoji.Count == 0 &&
                    _fruitModel.FruitEmoji.Count == 0)
                {
                    TempData["GameState"] = "Completed";
                    TempData["MatchResult"] = "Game over! All matches completed!";
                    // Redirect to a different action if you want to display a game completed view
                    // For now, it will just redirect to the Index view
                }
            }
            else
            {
                TempData["MatchResult"] = "Try again!";
            }

            TempData.Keep();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult RegisterUser(string username)
        {
            if (!string.IsNullOrWhiteSpace(username))
            {
                var user = _userModel.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
                if (user == null)
                {
                    user = new UserModel { Username = username, GamesPlayed = 0 };
                    _userModel.Add(user);
                }

                // Set the current player
                TempData["CurrentPlayer"] = user.Username;
            }

            return RedirectToAction(nameof(Index));
        }

        public void RemoveEmoji(string emoji)
        {
            _animalModel.AnimalEmoji.RemoveAll(e => e == emoji);
            _foodModel.FoodEmoji.RemoveAll(e => e == emoji);
            _fruitModel.FruitEmoji.RemoveAll(e => e == emoji);
        }

        public List<string> Shuffle(List<string> list)
        {
            var rng = new Random();
            return list.OrderBy(a => rng.Next()).ToList();
        }
    }
}
