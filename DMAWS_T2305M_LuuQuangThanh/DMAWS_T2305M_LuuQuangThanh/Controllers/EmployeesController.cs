using DMAWS_T2305M_LuuQuangThanh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMAWS_T2305M_KimQuangMinh.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly t2305mApiContext _context;

        public EmployeesController(t2305mApiContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.Include(e => e.ProjectEmployees).ToListAsync();
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        // PUT: api/Employees/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmployeeId) return BadRequest();

            _context.Entry(employee).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectDetails(int id)
        {
            var project = await _context.Projects
                                        .Include(p => p.ProjectEmployees)
                                        .ThenInclude(pe => pe.Employees)
                                        .FirstOrDefaultAsync(p => p.ProjectId == id);
            if (project == null) return NotFound();

            return project;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Employee>>> SearchEmployees(string name, DateTime? dobFrom, DateTime? dobTo)
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.EmployeeName.Contains(name));
            }

            if (dobFrom.HasValue)
            {
                query = query.Where(e => e.EmployeeDOB >= dobFrom.Value);
            }

            if (dobTo.HasValue)
            {
                query = query.Where(e => e.EmployeeDOB <= dobTo.Value);
            }

            return await query.Include(e => e.ProjectEmployees).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeDetails(int id)
        {
            var employee = await _context.Employees
                                         .Include(e => e.ProjectEmployees)
                                         .ThenInclude(pe => pe.Projects)
                                         .FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee == null) return NotFound();

            return employee;
        }

    }
}
