using SpionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpionAPI_Model.Models
{
    public class User
    {
        public int Id { get; set; }
        public String Name { get; set; } = "";
        public int Score { get; set; }


        public virtual IList<GuessData> GuessData { get; set; }

    }
}
