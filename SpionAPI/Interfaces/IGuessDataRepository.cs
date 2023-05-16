using SpionAPI.Models;

namespace SpionAPI.Interfaces
{
    public interface IGuessDataRepository : IGenericRepository<GuessData>
    {
        Task<bool> CheckGuessDuplicity(string GuessedWord, string RelatedWord);
    }
}
