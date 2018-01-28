using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angular.Controllers.Resources;
using Angular.Models;
using Angular.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Angular.Controllers
{   
    public class MakesController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;

        public MakesController(VegaDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }
       [HttpGet("api/makes")]
        // GET
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes =await context.Makes.Include(m => m.Models).ToListAsync();
            return mapper.Map<List<Make>, List<MakeResource>>(makes);
        }
    }
}