using System.ComponentModel.DataAnnotations;
using DMAWS_T2305M_LuuQuangThanh.Attributes;
namespace DMAWS_T2305M_LuuQuangThanh.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string EmployeeName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [AgeValidation(16)]
        public DateTime EmployeeDOB { get; set; }

        [Required]
        public string EmployeeDepartment { get; set; }  

        // Navigation property
        public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; }
    }
}
