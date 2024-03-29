﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeAppRestApi.Models;

namespace TimeAppRestApi.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LeaderId { get; set; }
        public List<int> Moderators { get; set;}
        public List<int> MembersIds { get; set; }
        public List<TeamNote> TeamNotes { get; set; }
        public List<Message> Messages { get; set; }

    }
}
