namespace SpionAPI.Models.Dto
{
    public static class DtoConvertGameStatistic
    {
        public static GameStatisticsDto ToGuessDataDto(GameStatistics gameStats)
        {
            if (gameStats == null) return null;

            return new GameStatisticsDto()
            {
                PlayerCount = gameStats.PlayerCount,
                GuessDataID = gameStats.GuessDataID,
                UseRelatedWordAsAHint = gameStats.UseRelatedWordAsAHint,
                SpyPresent = gameStats.SpyPresent,
                UndercoverPresent = gameStats.UndercoverPresent,
                PantonimePresent = gameStats.PantonimePresent,
                SingerPresent = gameStats.SingerPresent,
                SpyWon = gameStats.SpyWon,
                UndercoverWon = gameStats.UndercoverWon,
                GameCompleted = gameStats.GameCompleted,
                GameStarted = gameStats.GameStarted,


            };

        }

        public static GameStatistics ToGuessData(GameStatisticsDto gameStatsDto)
        {
            if (gameStatsDto == null) return null;

            return new GameStatistics()
            {
                Id = 0,
                PlayerCount = gameStatsDto.PlayerCount,
                GuessDataID = gameStatsDto.GuessDataID,
                UseRelatedWordAsAHint = gameStatsDto.UseRelatedWordAsAHint,
                SpyPresent = gameStatsDto.SpyPresent,
                UndercoverPresent = gameStatsDto.UndercoverPresent,
                PantonimePresent = gameStatsDto.PantonimePresent,
                SingerPresent = gameStatsDto.SingerPresent,
                SpyWon = gameStatsDto.SpyWon,
                UndercoverWon = gameStatsDto.UndercoverWon,
                GameCompleted = gameStatsDto.GameCompleted,
                GameStarted = gameStatsDto.GameStarted,
            };

        }

    }
}