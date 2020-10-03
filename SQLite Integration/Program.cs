using SQLiteLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_Integration
{
    class Program
    {
        static void Main(string[] args)
        {
            DebugCreateUser();
        }
        static void DebugCreateUser()
        {
            UserModel newUser = new UserModel();
            newUser.Username = "FUCKOOFF";
            newUser.Password = "yteee1";
            newUser.DateJoined = (DateTime.Now).ToString();

            SQLiteDataAccess.LoadUserData(newUser.Username, out int status2);

            if (status2 == 1)
            {

                SQLiteDataAccess.SaveNewUser(newUser, out int status);
                Debug.WriteLine(status);
            }
            else
            {
                Console.WriteLine("user already exists");
                Console.ReadLine();
            }
        }
    }
}
