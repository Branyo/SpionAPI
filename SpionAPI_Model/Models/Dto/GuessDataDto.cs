using System.ComponentModel.DataAnnotations;

namespace SpionAPI.Models
{
    public class GuessDataDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string GuessedWord { get; set; }
        
        [Required]
        public string RelatedWord { get; set; }


        //public GuessData ToGuessData() 
        //{
        //    return new GuessData()
        //    {
        //        Id = this.Id,
        //        GuessedWord = this.GuessedWord,
        //        RelatedWord = this.RelateWord
        //    };
            
        //}



    }
}
