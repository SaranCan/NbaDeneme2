using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NbaDeneme2.Models;

namespace NbaDeneme2.Controllers
{
    public class PlayersController : Controller
    {
        private readonly Nbadeneme2Context _context;

        public PlayersController(Nbadeneme2Context context)
        {
            _context = context;
        }

        // Top 10 by Points
        public async Task<IActionResult> Index()
        {
            var players = await _context.VwTopScorersPpgs.ToListAsync();
            return View(players);
        }

        // Top 10 by Assists
        public async Task<IActionResult> Assists()
        {
            var players = await _context.VwTopAssisterApgs.ToListAsync();
            return View(players);
        }

        // Top 10 by Rebounds
        public async Task<IActionResult> Rebounds()
        {
            var players = await _context.VwTopRebounderRpgs.ToListAsync();
            return View(players);
        }

        // Top 10 by Steals
        public async Task<IActionResult> Steals()
        {
            var players = await _context.VwTopStealerSpgs.ToListAsync();
            return View(players);
        }

        // Top 10 by Blocks
        public async Task<IActionResult> Blocks()
        {
            var players = await _context.VwTopBlockerBpgs.ToListAsync();
            return View(players);
        }

        // Top 10 by Salary
        public async Task<IActionResult> Salaries()
        {
            var players = await _context.VwTopSalaries.ToListAsync();
            return View(players);
        }
    }
}
