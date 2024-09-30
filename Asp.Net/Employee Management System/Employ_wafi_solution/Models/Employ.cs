using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Employ_wafi_solution.Models
{
    public class Employ
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }  
        public string LastName { get; set; }
        public string PhotoPath { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime DateOfBirth { get; set; }
        
    }
}
