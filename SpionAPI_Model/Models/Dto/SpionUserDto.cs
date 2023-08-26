using System.ComponentModel.DataAnnotations;

namespace SpionAPI.Models
{
    public class SpionUserDto
    {
        [Required]
        public String? UserName { get; set; } = "";

        public int GamesPlayed { get; set; } = 0;

        public virtual IList<GuessData>? GuessData { get; set; }

        [Required]
        [StringLength (15, ErrorMessage = "Your password is limited to {2} to {1} characters", MinimumLength = 6)]
        public string? Password { get; set; }

    }
}
