/*
 * Important Notes:
 * Return codes rules of thumb:
 *      Code 0 - Ran as expected
 *      +ve codes - Alternative outcomes, planned for - no issues with code possible issues with data
 *      -ve codes - Error in the code or serious issues with the database
*/




using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteLib
{
    public class SQLiteDataAccess
    {
        public static UserModel LoadUserData(string username, out int status) //Pulls login id data from the database and outputs to a UserModel
        {
            status = -1;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) //Loads connection to the SQLite Database
            {
                var outputEnum = cnn.Query<UserModel>($@"SELECT * FROM loginids WHERE username='{username}'", new DynamicParameters()); //saves the row to output with the username in a UserModel IEnum
                List<UserModel> outputList = outputEnum.ToList(); //Converts Ienum to list of type UserModel
                UserModel output = outputList[0]; //Takes the first (and only if everything is working) value of the list and stores in output
                UserModel listCheck = outputList[1]; //Takes the second value of the list and stores in a check
                if (username == listCheck.Username) //If there is more than one of the same username in the database
                {
                    status = -2; //Duplicate Usernames written to database
                }
                if (username != output.Username) //If the username of the input is not the same as the output
                {
                    status = 1; //Username not found in Database
                }
                else
                {
                    status = 0;
                }
                return output;
            } //Using this method ensures that by the time this curly brace is hit the connection is closed
        }



        public static List<BlogPost> GetBlogPosts(string username, out int status) //Pulls blog posts from a specific user
        {
            status = -1;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) //Loads connection to the SQLite Database
            {
                var outputEnum = cnn.Query<BlogPost>($@"SELECT * FROM {username}", new DynamicParameters()); //saves the row to output with the username in a BlogPost IEnum
                status = 0;
                return outputEnum.ToList();
            } //Using this method ensures that by the time this curly brace is hit the connection is closed
        }


        public static void SaveNewBlogPost(BlogPost input, out int status) //Writes a blog post to the database
        {
            status = -1;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) //Loads connection to the SQLite Database
            {
                cnn.Execute($@"inser into {input.Username} (title, body, time, id) values (@Title, @Body, @DateAndTime)", input); //Executes command
                status = 0;
            }
            
        }

        public static List<string> UsersList(out int status) //Ouputs list of all users
        {
            status = -1;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) //Loads connection to the SQLite Database
            {
                var output = cnn.Query<string>($@"SELECT Username FROM loginids", new DynamicParameters());
                status = 0;
                return output.ToList();
            }
        }



        public static void SaveNewUser(UserModel user, out int status)
        {
            status = -1;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) //Loads connection to the SQLite Database
            {
                cnn.Execute("insert into loginids (Username, Password, DateJoined) values (@Username, @Password, @DateJoined)", user); //Executes SQL command with parameter 'user'
                string cmd = //Declaration of command to create new table in database for the new user
                    $@"
                        CREATE TABLE {user.Username} (
                            title TEXT,
                            body  TEXT,
                            time  DATETIME,
                            id    INTEGER  PRIMARY KEY AUTOINCREMENT
                        );

                    ";
                cnn.Execute(cmd); //Executes command
                status = 0;
            } //Using this method ensures that by the time this curly brace is hit the connection is closed
        }

        private static string LoadConnectionString(string id = "Default") //Gets the connection path & Type from app.config
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
        
    }
}
