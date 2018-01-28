using System.Collections.Generic;
using System.Threading.Tasks;
using Angular.Controllers.Resources;
using Angular.Models;
using Angular.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Angular.Controllers
{
    public class FeaturesController : Controller
    {
        // GET
        private readonly VegaDbContext context;
        private readonly IMapper mapper;
        public FeaturesController(VegaDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet("/api/features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
        {
            var features = await context.Features.ToListAsync();
      
            return mapper.Map<List<Feature>, List<KeyValuePairResource>>(features); 
        }
    }
}