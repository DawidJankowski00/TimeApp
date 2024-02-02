using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeApp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Msg { get; set; }
        public User User { get; set; }


        public Color IsCurrentUser
        {
            get
            {
                if(User.Name == App.User.Name)
                {
                    return new Color(128, 0, 128);
                }
                return new Color(192, 192, 192);
            }
        }
    }
}
