using AmalgamateLabs.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AmalgamateLabs.Controllers
{
    public class StoreAppController : Controller
    {
        private readonly SQLiteDBContext _context;

        public StoreAppController(SQLiteDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeApp = await _context.StoreApps.SingleOrDefaultAsync(m => m.StoreAppId == id);
            if (storeApp == null)
            {
                return NotFound();
            }

            ViewData["Title"] = $"{storeApp.Title} | Amalgamate Labs";
            ViewData["Description"] = storeApp.Description;
            ViewData["Keywords"] = $"{storeApp.Title}, app, {storeApp.Description}";
            ViewData["URL"] = storeApp.CanonicalUrl();
            ViewData["OGImagePath"] = storeApp.OpenGraphPicture.ImagePath();

            return View(storeApp);
        }
    }
}
