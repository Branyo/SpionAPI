using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpionAPI.Models
{
    public class GameStatistics
    {
        [Key]
        public int Id { get; set; }

        public int PlayerCount { get; set; }

        [ForeignKey(nameof(GuessDataID))] 
        public int GuessDataID { get; set; }

        public bool UseRelatedWordAsAHint { get; set; } = false;

        public bool SpyPresent { get; set; }

        public bool UndercoverPresent { get; set; } = false;
        
        public bool PantonimePresent { get; set; } = false;

        public bool SingerPresent { get; set; } = false;

        public bool SpyWon { get; set; } = false;

        public bool UndercoverWon { get; set; } = false;

        public DateTime GameStarted { get; set; }

        public DateTime GameCompleted { get; set; }



    }
}
