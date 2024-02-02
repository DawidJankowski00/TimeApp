using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeAppRestApi.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Msg { get; set; }
        public User User { get; set; }
    }
}
