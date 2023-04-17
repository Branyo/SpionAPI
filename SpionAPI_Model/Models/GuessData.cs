using SpionAPI_Model.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpionAPI.Models
{
    public class GuessData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string GuessedWord { get; set; } = "";
        
        [Required]
        public string RelatedWord { get; set; } = "";


        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        
        
        public DateTime? LastGameUsageTime { get; set; }

        [ForeignKey(nameof(CreatedByUserId))]
        public int CreatedByUserId { get; set; }

        public User CreatedByUser { get; set; }



        //public GuessDataDto ToGuessDataDto()
        //{
        //    return new GuessDataDto()
        //    {
        //        Id = this.Id,
        //        GuessedWord = this.GuessedWord,
        //        RelatedWord = this.RelateWord
        //    };

        //}


    }
}
