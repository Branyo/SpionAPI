using Microsoft.EntityFrameworkCore;
using SpionAPI.Interfaces;
using SpionAPI.Models;
using SpionAPI_DataAccess.Data;

namespace SpionAPI.Repositories
{
    public class GameStatsRepository : GenericRepository<GameStatistics>, IGameStatsRepository
    {
        public GameStatsRepository(AppDbContext context) : base(context)
        {
        }
     
    }
}
