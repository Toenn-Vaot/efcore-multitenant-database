using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Multitenant.Api.Models;

namespace Multitenant.Api.Controllers
{
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly MultitenantContext _context;
        private readonly ILogger<ClientsController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="logger">The logger instance</param>
        public ClientsController(MultitenantContext context, ILogger<ClientsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("clients")]
        [ProducesResponseType(typeof(IEnumerable<ClientResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetAll()
        {
            await _context.Database.MigrateAsync();
            var response = await _context.Clients.ToListAsync();
            var result = response.ConvertAll(x => new ClientResource { Name = x.Name, BirthDate = x.BirthDate });

            if(result.Count > 0)
                return Ok(result);
            return NoContent();
        }

        [HttpGet("clients/{id:int}")]
        [ProducesResponseType(typeof(ClientResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            await _context.Database.MigrateAsync();
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            var result = new ClientResource { Name = client.Name, BirthDate = client.BirthDate };
            return Ok(result);
        }
    }
}
