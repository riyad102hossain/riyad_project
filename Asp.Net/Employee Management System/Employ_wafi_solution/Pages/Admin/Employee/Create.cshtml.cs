using Employ_wafi_solution.Models;
using Employ_wafi_solution.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Employ_wafi_solution.Pages.Admin.Employee
{
    public class CreateModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public DTOEmploy DTOEmploy { get; set; } = new DTOEmploy();

        public CreateModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }
        public void OnGet()
        {
        }

        public string errormessage = " ";
        public string successmessage = " ";
        public async Task<IActionResult> OnPostAsync()
        {
            // Validation part
            if (DTOEmploy.PhotoPath == null)
            {
                ModelState.AddModelError("DTOEmploy.PhotoPath", "This Image file required");
            }

            if (!ModelState.IsValid)
            {
                errormessage = "Provide all the Fields";
                return Page();
            }

            // save the image file
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(DTOEmploy.PhotoPath!.FileName);
            string imageFullPath = environment.WebRootPath + "/Image/" + newFileName;

            using (var stream = System.IO.File.Create(imageFullPath))
            {

                DTOEmploy.PhotoPath.CopyTo(stream);
            }

            //save employee in database
            Employ employ = new Employ()
            {
                FirstName = DTOEmploy.FirstName,
                LastName = DTOEmploy.LastName,
                Email = DTOEmploy.Email,
                Mobile = DTOEmploy.Mobile,
                DateOfBirth = DTOEmploy.DateOfBirth,
                PhotoPath = newFileName
            };

            var Name = string.Join(" ", employ.FirstName, employ.LastName);

            context.Employs.Add(employ);
            await context.SaveChangesAsync();
            return RedirectToPage("Index");

           // Clear form
            DTOEmploy.FirstName = "";
            DTOEmploy.LastName = "";
            DTOEmploy.PhotoPath = null;
            DTOEmploy.Email = "";
            DTOEmploy.Mobile = "";


            ModelState.Clear();
            successmessage = " Employee created successfully";
            Response.Redirect("/Admin/Employee/Index");

        }
    }
}
