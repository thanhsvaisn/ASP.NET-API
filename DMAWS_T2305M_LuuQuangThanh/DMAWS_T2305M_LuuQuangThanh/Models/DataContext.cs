using Microsoft.EntityFrameworkCore;

namespace DMAWS_T2305M_LuuQuangThanh.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        // Khai báo các DbSet tương ứng với các model của bạn
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ProjectEmployee> ProjectEmployees { get; set; }

        // Cấu hình quan hệ giữa các bảng (nếu cần thiết)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Thiết lập khoá chính cho bảng ProjectEmployee
            modelBuilder.Entity<ProjectEmployee>()
                .HasKey(pe => new { pe.EmployeeId, pe.ProjectId });

            // Thiết lập quan hệ một-nhiều giữa Project và ProjectEmployee
            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(pe => pe.Projects)
                .WithMany(p => p.ProjectEmployees)
                .HasForeignKey(pe => pe.ProjectId);

            // Thiết lập quan hệ một-nhiều giữa Employee và ProjectEmployee
            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(pe => pe.Employees)
                .WithMany(e => e.ProjectEmployees)
                .HasForeignKey(pe => pe.EmployeeId);
        }
    }
}
