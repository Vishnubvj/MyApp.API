using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.API.Data;
using MyApp.API.Models;

namespace MyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpCtlController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EmpCtlController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employee = await _context.Employees.ToListAsync();
            var Allemployee = from emp in employee
                       select emp;
            return Ok(Allemployee);

        }
        [HttpPost]
        public async Task<IActionResult> Post(EmployeeMdl emb)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid Data" });
            }
            await _context.Employees.AddAsync(emb);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Employee Added Successfully" });
        }
    }
}
