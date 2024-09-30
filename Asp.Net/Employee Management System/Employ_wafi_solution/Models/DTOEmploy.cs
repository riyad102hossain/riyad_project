using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Employ_wafi_solution.Models
{
	public class DTOEmploy
	{
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        [BindProperty]
        public IFormFile? PhotoPath { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
		public string Mobile { get; set; }
		public DateTime DateOfBirth { get; set; }
	}
}
