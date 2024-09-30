using Employ_wafi_solution.Models;
using Employ_wafi_solution.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employ_wafi_solution.Pages.Admin.Employee
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Employ> Employs { get; set; } = new List<Employ>();

        // Pagination properties
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;  

        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int StartItem { get; set; }
        public int EndItem { get; set; }

        private const int PageSize = 6;

        // Search properties 
        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? DateOfBirth { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Mobile { get; set; }

        // Sorting properties
        [BindProperty(SupportsGet = true)]
        public string SortColumn { get; set; } = "FirstName";  
        [BindProperty(SupportsGet = true)]
        public bool IsAscending { get; set; } = true;
  
        public string SortOrder { get; set; } = "asc";

        public async Task OnGetAsync()
        {
            // Query to get all employees
            var employQuery = _context.Employs.AsQueryable();

            // search filters
            if (!string.IsNullOrEmpty(Name))
            {
                employQuery = employQuery.Where(e => (e.FirstName + " " + e.LastName).Contains(Name));
            }
            if (DateOfBirth.HasValue)
            {
                employQuery = employQuery.Where(e => e.DateOfBirth == DateOfBirth.Value);
            }
            if (!string.IsNullOrEmpty(Email))
            {
                employQuery = employQuery.Where(e => e.Email.Contains(Email));
            }
            if (!string.IsNullOrEmpty(Mobile))
            {
                employQuery = employQuery.Where(e => e.Mobile.Contains(Mobile));
            }

            // Apply sorting
            employQuery = SortColumn switch
            {

                "FirstName" => SortOrder == "asc"
                    ? employQuery.OrderBy(e => e.FirstName)
        :           employQuery.OrderByDescending(e => e.FirstName),
                "DateOfBirth" => SortOrder == "asc"
                    ? employQuery.OrderBy(e => e.DateOfBirth)
                    : employQuery.OrderByDescending(e => e.DateOfBirth),
                "Email" => SortOrder == "asc"
                    ? employQuery.OrderBy(e => e.Email)
                    : employQuery.OrderByDescending(e => e.Email),
                "Mobile" => SortOrder == "asc"
                    ? employQuery.OrderBy(e => e.Mobile)
                    : employQuery.OrderByDescending(e => e.Mobile),
                _ => employQuery.OrderBy(e => e.FirstName) 

            };

            // total count of employees after filtering
            TotalItems = await employQuery.CountAsync();
        
            TotalPages = (int)Math.Ceiling(TotalItems / (double)PageSize);
         
            StartItem = (PageNumber - 1) * PageSize + 1;
            EndItem = Math.Min(PageNumber * PageSize, TotalItems);

            // Fetch only the employees for the current page using Skip and Take
            Employs = await employQuery
                            .Skip((PageNumber - 1) * PageSize)
                            .Take(PageSize)
                            .ToListAsync();
        }

    }

}
