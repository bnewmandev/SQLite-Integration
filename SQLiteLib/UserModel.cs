using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteLib
{
    public class UserModel
    {
        public UserModel()
        {
             IsLoggedOn = false;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string DateJoined { get; set; }
        public int Id { get; set; }
        public bool IsLoggedOn { get; set; }
    }
}
