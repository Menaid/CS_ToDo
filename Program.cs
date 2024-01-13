using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using var db = new BloggingContext();

/*  Kommentera ut från rad 8 till rad 37 efter man kört koden en gång.

var users = File.ReadAllLines("BlogInfo/user.csv");
var blogs = File.ReadAllLines("BlogInfo/blogs.csv");
var posts = File.ReadAllLines("BlogInfo/posts.csv");

db.Posts.ExecuteDelete();
db.Users.ExecuteDelete();
db.Blogs.ExecuteDelete();
foreach (var user in users.Skip(1))
{
    var split = user.Split(',');
    db.Users.Add(new User{UserId = int.Parse(split[0]), Username = split[1], Password = split[2]});
}


foreach (var blog in blogs.Skip(1))
{
    var splitBlog = blog.Split(',');
    db.Blogs.Add(new Blog{BlogId = int.Parse(splitBlog[0]), Url = splitBlog[1], Name = splitBlog[2]});
}


foreach (var post in posts.Skip(1))
{
    var splitPost = post.Split(',');
    db.Posts.Add(new Post{PostId = int.Parse(splitPost[0]), Title = splitPost[1], Content = splitPost[2], Published_On = DateOnly.Parse(splitPost[3]), BlogId = int.Parse(splitPost[4]), UserId = int.Parse(splitPost[5])});
}

db.SaveChanges();
 */

var blogar = db.Blogs;
Console.WriteLine("Blogs in database");
foreach (var blog in blogar)
{
    Console.WriteLine($"{blog.Name}");
    foreach (var post in blog.Posts)
    {
        Console.WriteLine($"{post.Title} - {post.User.Username} - {post.Published_On}");
    }

    Console.WriteLine("");
    
}

var dbUsers = db.Users;
Console.WriteLine("Users in database");
foreach (var user in dbUsers)
{
    Console.WriteLine($"{user.Username}");
    foreach (var post in user.Posts)
    {
        Console.WriteLine($"{post.Title} - {post.Blog.Name}");
    }

    Console.WriteLine("");
}
