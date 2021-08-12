using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebShell.Models;

namespace WebShell.Controllers.api
{
    [ApiController]
    [Route("api/[controller]")]
    public class DbInfoController : Controller // 
    {
        private CommandContext _dbContext;

        public DbInfoController(CommandContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Command>> Index()
        {
            return await _dbContext.Commands.ToListAsync();
        }
    }
}
