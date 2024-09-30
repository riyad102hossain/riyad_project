using FastFood.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FastFood.Web.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var claimsIdentity=(ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return View(_context.ApplicationUsers.Where(x=>x.Id!=claim.Value).ToList());
        }
    }
}
