using System.ComponentModel.DataAnnotations;
using DMAWS_T2305M_KimQuangMinh.Attributes;
namespace DMAWS_T2305M_KimQuangMinh.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required, StringLength(150, MinimumLength = 2)]
        public string ProjectName { get; set; }

        [Required]
        public DateTime ProjectStartDate { get; set; }

        [CompareDates]
        public DateTime? ProjectEndDate { get; set; } 

        // Navigation property to handle many-to-many relation with Employee
        public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; }
    }
}
