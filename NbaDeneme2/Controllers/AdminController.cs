using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NbaDeneme2.Models;

namespace NbaDeneme2.Controllers
{
    public class AdminController : Controller
    {
        private readonly Nbadeneme2Context _context;
        private readonly IConfiguration _configuration;

        public AdminController(Nbadeneme2Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Login sayfası
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("IsAdmin") == "true")
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var adminUser = _configuration["AdminCredentials:Username"];
            var adminPass = _configuration["AdminCredentials:Password"];

            if (username == adminUser && password == adminPass)
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Kullanıcı adı veya şifre yanlış.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // Admin ana sayfa - match listesi
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            var matches = await _context.VwMatchResults
                .OrderByDescending(m => m.MatchDate)
                .ToListAsync();
            return View(matches);
        }

        // Yeni maç ekle
        public async Task<IActionResult> AddMatch()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            ViewBag.Teams = await _context.Teams.OrderBy(t => t.TeamName).ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMatch(int homeTeamId, int awayTeamId, DateOnly matchDate, int homeScore, int awayScore)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            var match = new Match
            {
                HomeTeamId = homeTeamId,
                AwayTeamId = awayTeamId,
                MatchDate = matchDate,
                ArenaId = _context.Teams.Find(homeTeamId)!.ArenaId,
                SeasonId = 1,
                HomeScore = homeScore,
                AwayScore = awayScore
            };

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            // Her iki takımın oyuncularını bul ve 0 istatistikle ekle
            var homePlayers = await _context.Players
                .Where(p => p.TeamId == homeTeamId)
                .ToListAsync();

            var awayPlayers = await _context.Players
                .Where(p => p.TeamId == awayTeamId)
                .ToListAsync();

            foreach (var player in homePlayers.Concat(awayPlayers))
            {
                _context.PlayerGameStats.Add(new PlayerGameStat
                {
                    PersonId = player.PersonId,
                    MatchId = match.MatchId,
                    Points = 0,
                    Asists = 0,
                    Rebounds = 0,
                    Steals = 0,
                    Blocks = 0,
                    MinutesPlayed = 0
                });
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Match added! Enter player stats below.";
            return RedirectToAction("MatchStats", new { id = match.MatchId });
        }

        // Maç skor güncelle
        public async Task<IActionResult> EditMatch(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            var match = await _context.Matches.FindAsync(id);
            if (match == null) return NotFound();

            ViewBag.HomeTeamName = (await _context.Teams.FindAsync(match.HomeTeamId))?.TeamName;
            ViewBag.AwayTeamName = (await _context.Teams.FindAsync(match.AwayTeamId))?.TeamName;
            return View(match);
        }

        [HttpPost]
        public async Task<IActionResult> EditMatch(int matchId, int homeScore, int awayScore)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            var match = await _context.Matches.FindAsync(matchId);
            if (match == null) return NotFound();

            match.HomeScore = homeScore;
            match.AwayScore = awayScore;
            await _context.SaveChangesAsync();

            TempData["Success"] = "Skor güncellendi!";
            return RedirectToAction("Index");
        }

        // Maç sil
        public async Task<IActionResult> DeleteMatch(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                // Önce standings'i manuel güncelle
                if (match.HomeScore.HasValue && match.AwayScore.HasValue)
                {
                    var homeStats = await _context.TeamStats.FirstOrDefaultAsync(ts => ts.TeamId == match.HomeTeamId && ts.SeasonId == match.SeasonId);
                    var awayStats = await _context.TeamStats.FirstOrDefaultAsync(ts => ts.TeamId == match.AwayTeamId && ts.SeasonId == match.SeasonId);

                    if (homeStats != null)
                    {
                        homeStats.Wins -= match.HomeScore > match.AwayScore ? 1 : 0;
                        homeStats.Loses -= match.HomeScore < match.AwayScore ? 1 : 0;
                        homeStats.Differential -= (match.HomeScore - match.AwayScore) ?? 0;
                    }
                    if (awayStats != null)
                    {
                        awayStats.Wins -= match.AwayScore > match.HomeScore ? 1 : 0;
                        awayStats.Loses -= match.AwayScore < match.HomeScore ? 1 : 0;
                        awayStats.Differential -= (match.AwayScore - match.HomeScore) ?? 0;
                    }
                }

                // PlayerGameStats sil
                var stats = _context.PlayerGameStats.Where(s => s.MatchId == id);
                _context.PlayerGameStats.RemoveRange(stats);

                // Maçı sil
                _context.Matches.Remove(match);
                await _context.SaveChangesAsync();
            }

            TempData["Success"] = "Match deleted successfully!";
            return RedirectToAction("Index");
        }

        // Maç istatistiklerini görüntüle ve düzenle
        public async Task<IActionResult> MatchStats(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            var match = await _context.Matches.FindAsync(id);
            if (match == null) return NotFound();

            var homeTeam = await _context.Teams.FindAsync(match.HomeTeamId);
            var awayTeam = await _context.Teams.FindAsync(match.AwayTeamId);

            ViewBag.MatchId = id;
            ViewBag.MatchDate = match.MatchDate.ToString("dd/MM/yyyy");
            ViewBag.HomeTeam = homeTeam?.TeamName;
            ViewBag.AwayTeam = awayTeam?.TeamName;
            ViewBag.HomeScore = match.HomeScore;
            ViewBag.AwayScore = match.AwayScore;

            var stats = await _context.PlayerGameStats
                 .Where(s => s.MatchId == id)
                 .Include(s => s.Person)
                     .ThenInclude(p => p.Person)
                 .ToListAsync();

            return View(stats);
        }

        [HttpPost]
        public async Task<IActionResult> MatchStats(int matchId, List<int> statsIds,
            List<int> points, List<int> asists, List<int> rebounds,
            List<int> steals, List<int> blocks, List<int> minutes)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            for (int i = 0; i < statsIds.Count; i++)
            {
                var stat = await _context.PlayerGameStats.FindAsync(statsIds[i]);
                if (stat != null)
                {
                    stat.Points = points[i];
                    stat.Asists = asists[i];
                    stat.Rebounds = rebounds[i];
                    stat.Steals = steals[i];
                    stat.Blocks = blocks[i];
                    stat.MinutesPlayed = minutes[i];
                }
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Player stats updated successfully!";
            return RedirectToAction("MatchStats", new { id = matchId });
        }
    }
}
