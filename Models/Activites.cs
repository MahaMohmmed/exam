using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam.Models
{
    public class Activites
    {
        [Key]
        [Required]
        public int ActivitesId { get; set; }
        [Required (ErrorMessage = "Titel is requierd")]
        [Display(Name = "Titel: ")]

        public string Titel { get; set; }
        [Display(Name = "Date :")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name = "Time :")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        [Display(Name = " Duration: ")]
        public int Duration { get; set; }
        public string DurationType { get; set; }
        [Required (ErrorMessage = "Descripton is requierd")]
        [Display(Name = " Descripton: ")]
        public string Descripton { get; set; }
        public int UserId { get; set; }
        public User CreatedBy { get; set; }
        public List<Patricipanats> CommingUsers { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}