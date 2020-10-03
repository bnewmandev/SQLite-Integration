using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteLib
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string DateAndTime { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public void Write(UserModel user, BlogPost input, out int status) //Writes a blogpost to the database (if authorised)
        {
            status = -1;
            if(user.IsLoggedOn == true)
            {
                SQLiteDataAccess.SaveNewBlogPost(input, out int status1);

                if (status1 != 0)
                {
                    status = -2; //Error from SQLiteDataAccess.SaveNewBlogPost
                }

            }
            else
            {
                status = 1; //Unauthorised
            }
        }

    }
}
