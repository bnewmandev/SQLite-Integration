There are 3 classes within this class library
	SQLiteDataAccess (static)
	BlogPost
	UserModel

SQLiteDataAccess:

LoadUserData(string username, out int status)
	returns class UserModel for associated username
	status codes:
		0 - Account found and successfully written
		1 - Username not found
	   -1 - Connection to database failed
	   -2 - Duplicate usernames in database

SaveNewUser(UserModel user, out int status)
	saves user to the database
	status codes:
		0 - Success
	   -1 - Connection to database failed

UsersList(out int status)
	outputs List<string> for all usernames in database
	status codes:
		0 - Success
	   -1 - Connection to database failed

GetBlogPosts(string username, out int status)
	outputs List<BlogPost> for all entries in user's table in database
	status codes:
		0 - Success
	   -1 - Connection to database failed

SaveNewBlogPost(BlogPost input, out int status) (use BlogPost.Write instead)
	outputs BlogPost to the database
	status codes:
		0 - Success
	   -1 - Connection to database failed


BlogPost:

Write (UserModel user, BlogPost input, out int status)
	outputs valid blogpost to database (if looged in)
	status codes:
		0 - Success
		1 - Unauthorized
	   -1 - Code not run
	   -2 - Carried error from SaveNewBlogPost