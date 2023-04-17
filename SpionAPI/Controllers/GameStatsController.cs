using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpionAPI.Models;
using SpionAPI_DataAccess.Data;

namespace SpionAPI.Controllers
{
    [Route("api/GameStats")]
    [ApiController]
    public class GameStatsController : ControllerBase
    {

        AppDbContext _dbContext;

        public GameStatsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpPost(Name = "InsertGameStatistics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<GameStatistics> InsertGameStatistics([FromBody] GameStatistics gameStats)
        {

            //Bad incomming data         
            if (
                gameStats == null ||
                gameStats.GameStarted.ToUniversalTime() > gameStats.GameCompleted.ToUniversalTime() ||
                gameStats.GameCompleted.ToUniversalTime() > DateTime.Now.ToUniversalTime() ||
                (!gameStats.UndercoverPresent && !gameStats.SpyPresent) ||
                (gameStats.SpyWon && !gameStats.SpyPresent) ||
                (gameStats.UndercoverWon && !gameStats.UndercoverPresent)
            )
            {
                ModelState.AddModelError("Error", "Bad game statistics data!");
                return BadRequest(ModelState);
            }

            _dbContext.GameStatistics.Add(gameStats);
            _dbContext.SaveChanges();

            return CreatedAtRoute("GetGameStatistics", gameStats.Id, gameStats);

        }


        [HttpGet("{id:int}", Name = "GetGameStatistics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<GameStatistics> GetGameStatistics(int id)
        {

            if (id == 0)
            {
                return BadRequest();
            }

            GameStatistics gameStats = _dbContext.GameStatistics.FirstOrDefault(x => x.Id == id);

            if (gameStats == null)
            {
                return NotFound();
            }

            return Ok(gameStats);

        }


    }
}
