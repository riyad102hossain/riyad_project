using Employ_wafi_solution.Models;
using Employ_wafi_solution.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Linq;

namespace Employ_wafi_solution.Pages.Admin.Employee
{
    public class DeleteModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        public DeleteModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }

        [BindProperty]
        public Employ Employ { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/Admin/Employee/Index");
            }

            Employ = context.Employs.Find(id);

            if (Employ == null)
            {
                return RedirectToPage("/Admin/Employee/Index");
            }

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/Admin/Employee/Index");
            }

            var employ = context.Employs.Find(id);
            if (employ == null)
            {
                return RedirectToPage("/Admin/Employee/Index");
            }

            string imgpath = Path.Combine(environment.WebRootPath, "Image", employ.PhotoPath);
            if (System.IO.File.Exists(imgpath))
            {
                System.IO.File.Delete(imgpath);
            }

            context.Employs.Remove(employ);
            context.SaveChanges();

            return RedirectToPage("/Admin/Employee/Index");
        }
    }
}
