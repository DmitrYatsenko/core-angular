using System.Threading.Tasks;
using Angular.Migrations;
using Angular.Models;

namespace Angular.Persistence
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
    }
}