using FluentResults;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EngineeringWeekly.API.Componets.Interfaces
{
    public interface IStarWarsStarshipGetter
    {
        Task<Result<List<string>>> GetStarship();
        Task<Result<string>> GetStarship(int shipId);
    }
}
