using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpionAPI.Models;
using SpionAPI.Models.Dto;
using SpionAPI_DataAccess.Data;

namespace SpionAPI.Controllers
{

    [Route("api/GamePlay")]
    [ApiController]
    public class GamePlayController : ControllerBase
    {       

        AppDbContext _dbContext;

        public GamePlayController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        [HttpGet(Name = "GetRandomGameData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<GuessDataDto> GetRandomGameData()
        {
            int wordCountHalved = _dbContext.GuessData.Count() / 2;

            int randomId = new Random(DateTime.Now.Second * 1000 + DateTime.Now.Millisecond).Next(0, wordCountHalved ) + 1;

            //GuessData gData = _dbContext.GuessData.FirstOrDefault(x => x.Id == randomId);
            //GuessData gData = _dbContext.GuessData.OrderBy(x => x.LastGameUsage).Take(wordCountHalved).FirstOrDefault(x => x.Id == randomId);
            //GuessData gData = _dbContext.GuessData.OrderByDescending(x => x.LastGameUsage).Skip(wordCountHalved + randomId).Take(1).SingleOrDefault(x => x.Id == randomId);
            GuessData gData = _dbContext.GuessData.OrderBy(x => x.LastGameUsageTime).Take(randomId).LastOrDefault(); //  Skip(wordCountHalved + ).Take(1).SingleOrDefault(x => x.Id == randomId);
            
            if (gData == null)
            {
                return NotFound();
            }  
            
            GuessDataDto gDataDto = DtoConvert.ToGuessDataDto( gData );

            ////update last game usage date
            //gData.LastGameUsage = DateTime.Now;
            //_dbContext.GuessData.Update(gData);
            //_dbContext.SaveChanges();

            return Ok(gDataDto);
        }

        /// <summary>
        /// Update last game usage date = NOW in GuessData entity
        /// </summary>
        /// <param name="id">GuessData id parameter</param>
        /// <returns></returns>

        [HttpPut("{id:int}", Name = "UpdateLastGameUsageDate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateLastGameUsageDate(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            GuessData gData = _dbContext.GuessData.FirstOrDefault(x => x.Id == id);

            if (gData == null)
            {
                return NotFound();
            }
            
            gData.LastGameUsageTime = DateTime.Now;
            _dbContext.GuessData.Update(gData);
            _dbContext.SaveChanges();

            return NoContent();

        }


    }
}
