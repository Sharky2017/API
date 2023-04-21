using API_NASAT.Context;
using API_NASAT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_NASAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListUsers : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ListUsers(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<QueryUsers>>> GetUsuarios()
        {
            var listUsers = await _context.Usuarios.ToListAsync();
            return Ok(listUsers);
        }
    }
}
