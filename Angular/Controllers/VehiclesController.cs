using System;
using System.Threading.Tasks;
using Angular.Controllers.Resources;
using Angular.Models;
using Angular.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Angular.Controllers
{   [Route("api/vehicles")]  
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly VegaDbContext vegaDbContext;
        private readonly IVehicleRepository repository;


        public VehiclesController(IMapper mapper, VegaDbContext vegaDbContext, IVehicleRepository repository)
        {
            this.vegaDbContext = vegaDbContext;
            this.mapper = mapper;
            this.repository = repository;
        }   

        // GET
        /*public IActionResult Index()
        {
            return
            View();
        }*/
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody]VehicleResource vehicleResource)
        {   
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = await vegaDbContext.Models.FindAsync(vehicleResource.Id);
            if (model == null)
            {
                ModelState.AddModelError("Model", "Invalid ModelId");
                return BadRequest(ModelState);
            }
            var vehicle = mapper.Map<VehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;
                
            repository.Add(vehicle);
            await vegaDbContext.SaveChangesAsync();
            vehicle = await repository.GetVehicle(vehicle.Id); /*await vegaDbContext.Vehicles.Include(v=>v.Features)
                .ThenInclude(vf=>vf.Feature)
                .Include(m=>m.Model)
                .ThenInclude(m=>m.Make)
                .SingleOrDefaultAsync(v=>v.Id == vehicle.Id)*/;
            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id,[FromBody]VehicleResource vehicleResource)
        {  
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await repository.GetVehicle(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            mapper.Map<VehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;
                
           
            await vegaDbContext.SaveChangesAsync();
            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id, false);
            if (vehicle == null)
            {
                return NotFound();
            }
            repository.Remove(vehicle);
            await vegaDbContext.SaveChangesAsync();
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
           
            if(includeRelated != false)
                return await vegaDbContext.Vehicles.FindAsync(id);
            
            return await repository.GetVehicle(id);
            /*if (vehicle == null)
            {
                return NotFound();
            }*/
            /*var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(vehicleResource);*/
        }
    }
}