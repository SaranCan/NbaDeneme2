using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NbaDeneme2.Models;

namespace NbaDeneme2.Controllers
{
    public class TeamsController : Controller
    {
        private readonly Nbadeneme2Context _context;

        public TeamsController(Nbadeneme2Context context)
        {
            _context = context;
        }

        // Teams ana sayfa - League Standings
        public async Task<IActionResult> Index()
        {
            var standings = await _context.VwLeagueStandings.ToListAsync();
            return View(standings);
        }

        // Conference standings
        public async Task<IActionResult> Conference()
        {
            var east = await _context.VwEastStandings.ToListAsync();
            var west = await _context.VwWestStandings.ToListAsync();
            ViewBag.East = east;
            ViewBag.West = west;
            return View();
        }

        // Payroll
        public async Task<IActionResult> Payroll()
        {
            var payroll = await _context.VwTeamPayrolls.ToListAsync();
            return View(payroll);
        }

        // Team Roster
        public async Task<IActionResult> Roster(int id)
        {
            var roster = await _context.VwTeamRosters
                .Where(r => r.TeamId == id)
                .ToListAsync();
            ViewBag.TeamName = roster.FirstOrDefault()?.TeamName;
            return View(roster);
        }
    }
}
