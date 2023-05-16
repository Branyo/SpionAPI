using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SpionAPI.Interfaces;
using SpionAPI.Models;
using SpionAPI.Models.Dto;
using SpionAPI.Repositories;
using SpionAPI_DataAccess.Data;
using System.Security.Claims;

namespace SpionAPI.Controllers
{

    [Route("api/GuessEdit")]
    [ApiController]
    public class GuessEditController : ControllerBase
    {
        private readonly IGuessDataRepository _guessDataRepository;

        public GuessEditController(IGuessDataRepository guessDataRepository)
        {
            this._guessDataRepository = guessDataRepository;
        }

        [HttpGet(Name = "GetAllGuessData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GuessDataDto>>> GetAllGuessData()
        {
            var result = new List<GuessDataDto>();
            var data = await _guessDataRepository.GetAllAsync();            

            foreach (GuessData dRow in data)
            {
                result.Add(DtoConvert.ToGuessDataDto(dRow));
            }

            return Ok(result);                                
        }


        [HttpGet("{id:int}", Name = "GetGuessData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GuessDataDto>> GetGuessData(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var gData = await _guessDataRepository.GetAsync(id);

            if (gData == null)
            {
                return NotFound();
            }

            GuessDataDto gDataDto = DtoConvert.ToGuessDataDto(gData);
            return Ok(gData);

        }




        [HttpPost (Name = "CreateGuessData")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GuessDataDto>> CreateGuessData([FromBody] GuessDataDto guessDto ) 
        {
            //Null data incomming
            if (guessDto == null)
            {
                return BadRequest();
            }

            //TODO - fix the user auth stuff !!!!
            //Bad creator - user Id
            //if (_dbContext.Users.FirstOrDefault(x => x.Id == guessDto ) == null )
            //{
            //    ModelState.AddModelError("Error", "User doesn't exist")
            //    return BadRequest();
            //}

            //Guessed word cannot be the same as related word
            if (guessDto.GuessedWord.Trim().ToLower() == guessDto.RelatedWord.Trim().ToLower())
            {
                ModelState.AddModelError("Error", "Guessed word cannot be the same as related word");
                return BadRequest(ModelState);
            }

            //Word already in database
            if (await _guessDataRepository.CheckGuessDuplicity(guessDto.GuessedWord, guessDto.RelatedWord))
            {
                ModelState.AddModelError("Error", "This word is already in database");
                return BadRequest(ModelState);
            }

            //Add entity to the DB
            var gDataIn = DtoConvert.ToGuessData(guessDto);
            
            gDataIn.Id = 0;                                 //prevent bad ID input
            ///TODO - Replace with user auth logic!!!!
            gDataIn.CreatedByUserId = 1;            
            gDataIn.CreateTime = DateTime.Now;              //update create time

            await _guessDataRepository.AddAsync(gDataIn);

            return CreatedAtRoute("GetGuessData", new { id = guessDto.Id }, guessDto );        
        
        }


        [HttpDelete("{id:int}", Name = "DeleteGuessData")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteGuessData( int id ) 
        {
            if (id == 0)
            {
                return BadRequest();
            }

            _guessDataRepository.DeleteAsync(id);
           
            return NoContent();                
        }


        [HttpPut("{id:int}", Name = "UpdateGuessData")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateGuessData(int id, [FromBody] GuessDataDto guessDto)
        {
            if (guessDto == null || guessDto.Id == 0 || guessDto.Id != id)
            {
                return BadRequest();
            }

            var gData = await _guessDataRepository.GetAsync(id);

            if (gData == null)
            {
                return NotFound();
            };


            var gD = DtoConvert.ToGuessData(guessDto);
            gD.UpdateTime = DateTime.Now;                   //update date of the change
                        
            await _guessDataRepository.UpdateAsync(gD);            
            
            return NoContent();

        }


    }
}
