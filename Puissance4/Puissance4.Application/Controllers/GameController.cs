using Microsoft.AspNetCore.Mvc;
using Puissance4.DataAccess;

namespace Puissance4.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly Puissance4DbContext _context;

        public GameController(Puissance4DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetGames()
        {
            var games = _context.Games.ToList();
            return Ok(games);
        }

        [HttpPost]
        public IActionResult CreateGame()
        {
            var newGame = new Game
            {
                State = "Empty",
                CreatedAt = DateTime.UtcNow
            };
            _context.Games.Add(newGame);
            _context.SaveChanges();
            return Ok(newGame);
        }
    }
}