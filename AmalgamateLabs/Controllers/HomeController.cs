using AmalgamateLabs.Base;
using AmalgamateLabs.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AmalgamateLabs.Controllers
{
    public class HomeController : Controller
    {
        private readonly SQLiteDBContext _context;
        private readonly ILogger _logger;

        public HomeController(SQLiteDBContext context, AppLogger applogger)
        {
            _context = context;
            _logger = applogger.Logger;
        }

        public IActionResult Index()
        {
            ViewData["Blogs"] = _context.Blogs.ToList();
            ViewData["WebGLGames"] = _context.WebGLGames.ToList();

            return View(new ViewModel(_context, nameof(ContactForm)));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            if (!statusCode.HasValue)
            {
                statusCode = 404;
            }

            HttpContext.Response.StatusCode = statusCode.Value;

            //TODO: Can probably replace this with System.Net.HttpStatusCode somehow.
            XDocument httpStatusCodes = XDocument.Parse(_context.SystemConfigs.OrderBy(sc => sc.SystemConfigId).Last().HTTPStatusCodes);

            XElement httpStatusCodeElement = httpStatusCodes
                .Descendants()
                .FirstOrDefault(el => el.Attribute("code")?.Value == statusCode.ToString());

            ViewData["ErrorCode"] = statusCode.ToString();
            ViewData["ErrorDescription"] = httpStatusCodeElement != null ? httpStatusCodeElement.Attribute("description").Value : string.Empty;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitContactForm([Bind("ContactFormId,Name,UserEmailAddress,Subject,MessageBody")] ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                bool success = await contactForm.Submit(_context.SystemConfigs.OrderBy(sc => sc.SystemConfigId).Last(), _logger);

                if (success)
                {
                    await _context.SaveChangesAsync();
                }

            }

            return RedirectToAction("Index");
        }
    }
}
