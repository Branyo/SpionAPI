using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpionAPI.Interfaces;
using SpionAPI.Models;
using SpionAPI.Models.Dto;
using SpionAPI_DataAccess.Data;

namespace SpionAPI.Controllers
{

    [Route("api/GamePlay")]
    [ApiController]
    public class GamePlayController : ControllerBase
    {
        private readonly IGamePlayRepository _gamePlayRepository;

        public GamePlayController(IGamePlayRepository gamePlayRepository)
        {
            this._gamePlayRepository = gamePlayRepository;
        }
               

        [HttpGet(Name = "GetRandomGameData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GuessDataDto>> GetRandomGameData()
        {

            GuessData gData = await _gamePlayRepository.GetRandomRecord();            

            if (gData == null)
            {
                return NotFound();
            }  
            
            GuessDataDto gDataDto = DtoConvertGuessData.ToGuessDataDto( gData );

            return Ok(gDataDto);
        }

        /// <summary>
        /// Update last game usage date = NOW in GuessData entity to prevent repeating recent words
        /// </summary>
        /// <param name="id">GuessData id parameter</param>
        /// <returns></returns>

        [HttpPut("{id:int}", Name = "UpdateLastGameUsageDate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateLastGameUsageDate(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            GuessData gData = await _gamePlayRepository.GetAsync(id);

            if (gData == null)
            {
                return NotFound();
            }
            
            gData.LastGameUsageTime = DateTime.Now;
            
            await _gamePlayRepository.UpdateAsync(gData);

            return NoContent();

        }


    }
}
