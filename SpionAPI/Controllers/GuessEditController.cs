using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SpionAPI.Models;
using SpionAPI.Models.Dto;
using SpionAPI_DataAccess.Data;
using System.Security.Claims;

namespace SpionAPI.Controllers
{

    [Route("api/GuessEdit")]
    [ApiController]
    public class GuessEditController : ControllerBase
    {
        AppDbContext _dbContext;

        public GuessEditController(AppDbContext dbContext )
        {
            this._dbContext = dbContext;
        }

        [HttpGet(Name = "GetAllGuessData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<GuessDataDto>> GetAllGuessData()
        {
            List<GuessDataDto> result = new List<GuessDataDto>();

            foreach (GuessData gdRow in _dbContext.GuessData)
            {
                result.Add(DtoConvert.ToGuessDataDto(gdRow));
            }

            return Ok(result);                                
        }


        [HttpGet("{id:int}", Name = "GetGuessData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<GuessDataDto> GetGuessData(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var gData = _dbContext.GuessData.FirstOrDefault(x => x.Id == id);

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
        public ActionResult<GuessDataDto> CreateGuessData([FromBody] GuessDataDto guessDto ) 
        {
            //Nulll data incomming
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


            //Word already in database
            if (_dbContext.GuessData.FirstOrDefault(x => ( 
                x.GuessedWord.ToLower() == guessDto.GuessedWord.ToLower() &&
                x.RelatedWord.ToLower() == guessDto.RelatedWord.ToLower() )                
                ) != null )
            {
                ModelState.AddModelError("Error", "This word is already in database");
                return BadRequest(ModelState);
            }

            //Guessed word cannot be the same as related word
            if ( guessDto.GuessedWord.Trim().ToLower() == guessDto.RelatedWord.Trim().ToLower() )
            {
                ModelState.AddModelError("Error", "Guessed word cannot be the same as related word");
                return BadRequest(ModelState);
            }

            GuessData gDataIn = DtoConvert.ToGuessData(guessDto);


            //To replace with user auth logic!!!!
            gDataIn.Id = 0;
            gDataIn.CreatedByUserId = 1;
            gDataIn.CreateTime = DateTime.Now;

            _dbContext.GuessData.Add(gDataIn);
            _dbContext.SaveChanges();

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

            GuessData guess = _dbContext.GuessData.FirstOrDefault(x => x.Id == id );

            if (guess == null)
            {
                return NotFound();
            }

            //remove
            _dbContext.GuessData.Remove(guess);
            _dbContext.SaveChanges();
           
            return NoContent();        
        
        }


        [HttpPut("{id:int}", Name = "UpdateGuessData")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateGuessData(int id, [FromBody] GuessDataDto guessDto)
        {
            if (guessDto == null || guessDto.Id == 0 || guessDto.Id != id)
            {
                return BadRequest();
            }

            //GuessData gDataOriginal = _dbContext.GuessData.FirstOrDefault(x => x.Id == id);
            GuessData gdataModified = _dbContext.GuessData.Find(id);
            
            if (gdataModified == null) 
            {
                return NotFound();
            };

            gdataModified.GuessedWord = guessDto.GuessedWord;
            gdataModified.RelatedWord = guessDto.RelatedWord;
            gdataModified.UpdateTime = DateTime.Now;
                        
            _dbContext.GuessData.Update(gdataModified);
            _dbContext.SaveChanges();
            //_dbContext.Entry();
            return NoContent();

        }


    }
}
