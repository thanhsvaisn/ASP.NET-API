using DMAWS_T2305M_KimQuangMinh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMAWS_T2305M_KimQuangMinh.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private readonly t2305mApiContext _context;

        public ProjectsController(t2305mApiContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await _context.Projects.Include(p => p.ProjectEmployees).ToListAsync();
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject", new { id = project.ProjectId }, project);
        }

        // PUT: api/Projects/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.ProjectId) return BadRequest();

            _context.Entry(project).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Projects/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Project>>> SearchProjects(string name, string status)
        {
            var query = _context.Projects.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.ProjectName.Contains(name));
            }

            if (status == "inprogress")
            {
                query = query.Where(p => p.ProjectEndDate == null || p.ProjectEndDate > DateTime.Now);
            }
            else if (status == "finished")
            {
                query = query.Where(p => p.ProjectEndDate != null && p.ProjectEndDate <= DateTime.Now);
            }

            return await query.Include(p => p.ProjectEmployees).ToListAsync();
        }

    }
}
