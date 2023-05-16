using Microsoft.EntityFrameworkCore;
using SpionAPI.Interfaces;
using SpionAPI.Models;
using SpionAPI_DataAccess.Data;

namespace SpionAPI.Repositories
{
    public class GuessDataRepository : GenericRepository<GuessData>, IGuessDataRepository
    {
        public GuessDataRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckGuessDuplicity(string GuessedWord, string RelatedWord)
        {

            return await _context.Set<GuessData>().FirstOrDefaultAsync(x => (
                x.GuessedWord.ToLower() == GuessedWord.ToLower() &&
                x.RelatedWord.ToLower() == RelatedWord.ToLower())) != null;
        }
    }
}
