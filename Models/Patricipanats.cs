using System.ComponentModel.DataAnnotations;

namespace exam.Models
{
    public class Patricipanats
    {
        [Key]
        public int PatricipanatsID { get; set; } 
        public int UserId { get; set; }
        public User Gest { get; set; }
        public int ActivitesId { get; set; }
        public Activites GestOf { get; set; }
    }
}