using FluentResults;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EngineeringWeekly.API.Componets.Interfaces
{
    public interface IStarWarsPeopleGetter
    {
        Task<Result<List<string>>> GetPeople();
        Task<Result<string>> GetPeople(int peopleId);
    }
}
