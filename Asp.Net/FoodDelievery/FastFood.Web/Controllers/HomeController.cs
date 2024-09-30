using FastFood.Models;
using FastFood.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;

namespace FastFood.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private object _context;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            ItemListViewModel vm = new ItemListViewModel()
            {
                Items = await _context.Items.Include(x => x.Category).Include(y => y.SubCategory),
                Categories = await _context.Categories.ToListAsync(),
            Coupons = await _context.Coupons.Where(c => c.IsActive==true).ToListAsync()
            };

            return View(vm);
        }
           
        public IActionResult Privacy()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    }

    internal class ItemListViewModel
    {
        public ItemListViewModel()
        {
        }

        public object Items { get; set; }
        public object Categories { get; set; }
        public object Coupons { get; set; }
    }
}