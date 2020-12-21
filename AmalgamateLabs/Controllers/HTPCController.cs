using AmalgamateLabs.Base;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Linq;
using System.Threading.Tasks;

namespace AmalgamateLabs.Controllers
{
    public class HTPCController : Controller
    {
        private readonly SQLiteDBContext _context;
        private readonly ILogger _logger;

        public HTPCController(SQLiteDBContext context, AppLogger applogger)
        {
            _context = context;
            _logger = applogger.Logger;
        }

        public async Task<IActionResult> Index()
        {
            return View((await _context.GetMovies(_logger)).Take(6));
        }
    }
}
