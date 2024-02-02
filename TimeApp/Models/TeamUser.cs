using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeApp.Models
{
    public class TeamUser : User
    {

        public TeamUser(User user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.Email = user.Email;
            this.Password = user.Password;
            this.Notes = user.Notes;
            this.GroupsId = user.GroupsId;
        }
        public string Rank {  get; set; }
    }
}
