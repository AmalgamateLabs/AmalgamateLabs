using AmalgamateLabs.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AmalgamateLabs.Controllers
{
    public class AppsController : Controller
    {
        private readonly SQLiteDBContext _context;

        public AppsController(SQLiteDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webGLGame = await _context.WebGLGames.SingleOrDefaultAsync(m => m.WebGLGameId == id);
            if (webGLGame == null)
            {
                return NotFound();
            }

            ViewData["Title"] = $"{webGLGame.Title} | Amalgamate Labs";
            ViewData["Description"] = webGLGame.Description;
            ViewData["Keywords"] = $"{webGLGame.Title}, free game, {webGLGame.Description}";
            ViewData["URL"] = webGLGame.CanonicalUrl();
            ViewData["OGImagePath"] = webGLGame.OpenGraphPicture.ImagePath();

            return View(webGLGame);
        }
    }
}
