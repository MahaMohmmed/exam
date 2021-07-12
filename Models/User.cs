
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [Display(Name = " Name: ")]
        [MinLength(2, ErrorMessage = "Name must be 2 characters or longer!")]

        public string Name { get; set; }
        
        [Required]
        [Display(Name = " Email: ")]

        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"(?=.*\d)(?=.*[A-Za-z])(?=.*?[^a-zA-Z0-9]).{8,}", ErrorMessage = "passWord should have number Symbols and leeters")]
        [Display(Name = " Password: ")]
        [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
        public string Password { get; set; }

        [NotMapped]
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm PW: ")]

        public string Confirm { get; set; }
        public List<Activites> CreatedActivites { get; set; }
        public List<Patricipanats> PatricipanatedActivites { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}