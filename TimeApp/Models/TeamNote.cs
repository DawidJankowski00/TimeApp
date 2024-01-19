using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeAppRestApi.Models;

namespace TimeApp.Models
{
    public class TeamNote
    {
        public int Id { get; set; }
        public Note Note { get; set; }

        public List<int> UserIds { get; set; }

    }
}
