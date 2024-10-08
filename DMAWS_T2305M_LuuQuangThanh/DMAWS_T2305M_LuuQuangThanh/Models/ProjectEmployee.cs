namespace DMAWS_T2305M_LuuQuangThanh.Models
{
    public class ProjectEmployee
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }  
        public string Tasks { get; set; }  

        // Navigation properties
        public virtual Employee Employees { get; set; }  
        public virtual Project Projects { get; set; }  
    }
}
