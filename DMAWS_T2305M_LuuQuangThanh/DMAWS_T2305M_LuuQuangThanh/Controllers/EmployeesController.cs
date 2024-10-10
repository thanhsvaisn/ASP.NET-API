using DMAWS_T2305M_LuuQuangThanh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMAWS_T2305M_LuuQuangThanh.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly DataContext _context;

        public EmployeesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _context.Employees.Include(e => e.ProjectEmployees).ToListAsync();
            return Ok(employees);
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<IActionResult> PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployeeDetails), new { id = employee.EmployeeId }, employee);
        }

        // PUT: api/Employees/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmployeeId) return BadRequest();

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Employees.Any(e => e.EmployeeId == id)) return NotFound();
                throw;
            }

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

        // GET: api/Employees/projects/{id}
        [HttpGet("projects/{id}")]
        public async Task<IActionResult> GetProjectDetails(int id)
        {
            var project = await _context.Projects
                                        .Include(p => p.ProjectEmployees)
                                        .ThenInclude(pe => pe.Employees)
                                        .FirstOrDefaultAsync(p => p.ProjectId == id);
            if (project == null) return NotFound();

            return Ok(project);
        }

        // GET: api/Employees/search
        [HttpGet("search")]
        public async Task<IActionResult> SearchEmployees(string name, DateTime? dobFrom, DateTime? dobTo)
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

            var result = await query.Include(e => e.ProjectEmployees).ToListAsync();
            return Ok(result);
        }

        // GET: api/Employees/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeDetails(int id)
        {
            var employee = await _context.Employees
                                         .Include(e => e.ProjectEmployees)
                                         .ThenInclude(pe => pe.Projects)
                                         .FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee == null) return NotFound();

            return Ok(employee);
        }
    }
}