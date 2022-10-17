using FluentResults;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EngineeringWeekly.API.Componets.Interfaces
{
    public interface IStarWarsPlanetGetter
    {
        Task<Result<List<string>>> GetPlanet();
        Task<Result<string>> GetPlanet(int planetId);
    }
}
