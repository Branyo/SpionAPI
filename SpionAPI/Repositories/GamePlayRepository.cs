using Microsoft.EntityFrameworkCore;
using SpionAPI.Interfaces;
using SpionAPI.Models;
using SpionAPI_DataAccess.Data;

namespace SpionAPI.Repositories
{
    public class GamePlayRepository : GenericRepository<GuessData>, IGamePlayRepository
    {
        public GamePlayRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<GuessData> GetRandomRecord() 
        {
            int wordCountHalved = _context.GuessData.Count() / 2;
            int randomId = new Random(DateTime.Now.Second * 1000 + DateTime.Now.Millisecond).Next(0, wordCountHalved) + 1;

            return await _context.GuessData.OrderBy(x => x.LastGameUsageTime).Take(randomId).LastOrDefaultAsync();

        }

    }
}
