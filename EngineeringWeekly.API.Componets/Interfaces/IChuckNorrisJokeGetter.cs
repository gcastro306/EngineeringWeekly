using FluentResults;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EngineeringWeekly.API.Componets.Interfaces
{
    public interface IChuckNorrisJokeGetter
    {
        Task<Result<List<string>>> GetJokeCategories();
        Task<Result<string>> GetJoke(string? category);
    }
}
