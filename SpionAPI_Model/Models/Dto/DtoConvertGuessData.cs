namespace SpionAPI.Models.Dto
{
    public static class DtoConvertGuessData
    {
        public static GuessDataDto ToGuessDataDto(GuessData guessData)
        {
            if (guessData == null) return null;

            return new GuessDataDto()
            {
                Id = guessData.Id,
                GuessedWord = guessData.GuessedWord,
                RelatedWord = guessData.RelatedWord
            };

        }

        public static GuessData ToGuessData(GuessDataDto guessDataDto)
        {
            if (guessDataDto == null) return null;
            
            return new GuessData()
            {
                Id = guessDataDto.Id,
                GuessedWord = guessDataDto.GuessedWord,
                RelatedWord = guessDataDto.RelatedWord
            };

        }



    }
}
