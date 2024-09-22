using Employ_wafi_solution.Models;
using Employ_wafi_solution.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace Employ_wafi_solution.Pages.Admin.Employee
{
    public class EditModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public DTOEmploy DTOEmploy { get; set; } = new DTOEmploy();
        public Employ Employ { get; set; } = new Employ();

        public string errormessage = "";
        public string successmessage = "";

        public EditModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }
        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/Employee/Index");
                return;
            }

            var employ = context.Employs.Find(id);
            if (employ == null)
            {
                Response.Redirect("/Admin/Employee/Index");
                return;
            }

            DTOEmploy.FirstName = employ.FirstName;
            DTOEmploy.LastName = employ.LastName;
            DTOEmploy.Email = employ.Email;
            DTOEmploy.Mobile = employ.Mobile;
            DTOEmploy.DateOfBirth = employ.DateOfBirth;

            Employ = employ;

        }
        public void OnPost(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/Employee/Index");
                return;
            }

            if (!ModelState.IsValid)
            {
                errormessage = " Provide the required fields.";
                return;
            }
            var employ = context.Employs.Find(id);
            if (employ == null)
            {
                Response.Redirect("/Admin/Employee/Index");
                return;
            }

            //image file update
            string newFileName = employ.PhotoPath;
            if (DTOEmploy.PhotoPath != null)
            {

                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(DTOEmploy.PhotoPath!.FileName);

                string imgpath = environment.WebRootPath + "/Image/" + newFileName;

                using (var stream = System.IO.File.Create(imgpath))
                {
                    DTOEmploy.PhotoPath.CopyTo(stream);

                }

                //delete old image
                string oldimgpath = environment.WebRootPath + "/Image/" + employ.PhotoPath;
                System.IO.File.Delete(oldimgpath);
            }

            //update database
            employ.PhotoPath = newFileName;
            employ.FirstName = DTOEmploy.FirstName ?? employ.FirstName;
            employ.LastName = DTOEmploy.LastName;
            employ.Email = DTOEmploy.Email;
            employ.Mobile = DTOEmploy.Mobile;
            employ.DateOfBirth = DTOEmploy.DateOfBirth;

            context.SaveChanges();

            Employ = employ;
            successmessage = "Employee Edited successfully";
            Response.Redirect("/Admin/Employee/Index");

        }
    }
}
