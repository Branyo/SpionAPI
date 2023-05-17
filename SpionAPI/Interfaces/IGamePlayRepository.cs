using Microsoft.EntityFrameworkCore;
using SpionAPI.Models;

namespace SpionAPI.Interfaces
{
    public interface IGamePlayRepository : IGenericRepository<GuessData>
    {
        public Task<GuessData> GetRandomRecord();        

    }
}
