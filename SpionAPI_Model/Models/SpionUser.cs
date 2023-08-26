using Microsoft.AspNetCore.Identity;
using SpionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpionAPI_Model.Models
{
    public class SpionUser : IdentityUser
    {
        public int GamesPlayed { get; set; }
        public virtual IList<GuessData> GuessData { get; set; }

    }
}
