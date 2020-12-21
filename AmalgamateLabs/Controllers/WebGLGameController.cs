using AmalgamateLabs.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AmalgamateLabs.Controllers
{
    public class WebGLGameController : Controller
    {
        private readonly SQLiteDBContext _context;

        public WebGLGameController(SQLiteDBContext context)
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

        public async Task<IActionResult> Play(int? id)
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

            string unityDataPath = $"../../WebGL_Games/{webGLGame.URLSafeTitle}/Release/{webGLGame.URLSafeTitle}.";
            ViewData["UnityDataURL"] = $"{unityDataPath}data";
            ViewData["UnityCodeURL"] = $"{unityDataPath}js";
            ViewData["UnityAsmURL"] = $"{unityDataPath}asm.js";
            ViewData["UnityMemURL"] = $"{unityDataPath}mem";

            return View(webGLGame);
        }
    }
}
