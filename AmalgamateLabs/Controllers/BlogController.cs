using AmalgamateLabs.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AmalgamateLabs.Controllers
{
    public class BlogController : Controller
    {
        private readonly SQLiteDBContext _context;

        public BlogController(SQLiteDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.BlogId == id);

            if (blog == null)
            {
                return NotFound();
            }

            ViewData["Title"] = $"{blog.Title} | Amalgamate Labs";
            ViewData["Description"] = blog.Description;
            ViewData["Keywords"] = blog.Keywords;
            ViewData["URL"] = blog.CanonicalUrl();
            ViewData["OGImagePath"] = blog.OpenGraphPicture.ImagePath();

            return View(blog);
        }
    }
}
