using System.Threading.Tasks;
using Angular.Models;
using Microsoft.EntityFrameworkCore;

namespace Angular.Persistence
{
    public class VehicleRepository: IVehicleRepository
    
    {
        private readonly VegaDbContext context;

        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;
        }
        public async Task<Vehicle> GetVehicle(int id, bool includeRelated)
        {
              return await context.Vehicles.Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
                .Include(m => m.Model)
                .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        

        /* public async Task<Vehicle> GetVehicleWithMake(int id)
        {
        }*/

        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
        }
    }
}