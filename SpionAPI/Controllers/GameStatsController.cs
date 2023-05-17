using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpionAPI.Interfaces;
using SpionAPI.Models;
using SpionAPI.Models.Dto;
using SpionAPI_DataAccess.Data;

namespace SpionAPI.Controllers
{
    [Route("api/GameStats")]
    [ApiController]
    public class GameStatsController : ControllerBase
    {
        
        private readonly IGameStatsRepository _gameStatsRepository;

        public GameStatsController(IGameStatsRepository gameStatsRepository)
        {
            this._gameStatsRepository = gameStatsRepository;
        }


        [HttpPost(Name = "CreateGameStatistics")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GameStatistics>> CreateGameStatistics([FromBody] GameStatisticsDto gameStatsDto)
        {

            //Bad incomming data         
            if (
                gameStatsDto == null ||
                gameStatsDto.GameStarted.ToUniversalTime() > gameStatsDto.GameCompleted.ToUniversalTime() ||
                gameStatsDto.GameCompleted.ToUniversalTime() > DateTime.Now.ToUniversalTime() ||
                (!gameStatsDto.UndercoverPresent && !gameStatsDto.SpyPresent) ||
                (gameStatsDto.SpyWon && !gameStatsDto.SpyPresent) ||
                (gameStatsDto.UndercoverWon && !gameStatsDto.UndercoverPresent)
            )
            {
                ModelState.AddModelError("Error", "Bad game statistics data!");
                return  BadRequest(ModelState);
            }

            var gameStats = DtoConvertGameStatistic.ToGuessData(gameStatsDto);
            await _gameStatsRepository.AddAsync( gameStats );
            
            return CreatedAtRoute("GetGameStatistics", new { id = gameStats.Id }, gameStatsDto);

        }


        [HttpGet("{id:int}", Name = "GetGameStatistics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GameStatisticsDto>> GetGameStatistics(int id)
        {

            if (id == 0)
            {
                return BadRequest();
            }

            GameStatistics gameStats = await _gameStatsRepository.GetAsync(id);
            GameStatisticsDto gameStatsDto = DtoConvertGameStatistic.ToGuessDataDto(gameStats);

            if (gameStatsDto == null)
            {
                return NotFound();
            }

            return Ok(gameStatsDto);

        }


    }
}
