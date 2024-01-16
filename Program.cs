using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using var db = new BloggingContext();

db.Posts.ExecuteDelete();
db.Users.ExecuteDelete();
db.Blogs.ExecuteDelete();

var users = File.ReadAllLines("BlogInfo/user.csv");
var blogs = File.ReadAllLines("BlogInfo/blogs.csv");
var posts = File.ReadAllLines("BlogInfo/posts.csv");

foreach (var line in users.Skip(1))
{
    var split = line.Split(',');
    var userId = int.Parse(split[0]);
    User? user = db.Users?.Find(userId);
    if (user != null)
    {
        continue;
    }

    db.Users?.Add(new User
    {
        UserId = userId, 
        Username = split[1], 
        Password = split[2]
    });
}

Console.WriteLine("");
Console.WriteLine("");
foreach (var line in blogs.Skip(1))
{
    var splitBlog = line.Split(',');
    var blogId = int.Parse(splitBlog[0]);
    Blog? blog = db.Blogs?.Find(blogId);
    if (blog != null)
    {
        continue;
    }

    db.Blogs?.Add(new Blog
    {
        BlogId = blogId, 
        Url = splitBlog[1], 
        Name = splitBlog[2]
    });
    db.SaveChanges();
}

Console.WriteLine("");
Console.WriteLine("");

foreach (var post in posts.Skip(1))
{
    var splitPost = post.Split(',');
    var postId = int.Parse(splitPost[0]);
    db.Posts?.Add(new Post
    {
        PostId = postId, 
        Title = splitPost[1], 
        Content = splitPost[2], 
        Published_On = DateOnly.Parse(splitPost[3]), 
        BlogId = int.Parse(splitPost[4]), 
        UserId = splitPost.Length == 6 ? int.Parse(splitPost[5]) : null 
    });
    db.SaveChanges();
}


Console.WriteLine("Blogs in database");
Console.WriteLine("");
foreach (var blog in db.Blogs!)
{
    Console.WriteLine("-----------------------------------------");
    Console.WriteLine();
    Console.WriteLine($"{blog.Name}");
    foreach (var post in blog.Posts)
    {
        Console.WriteLine($"{post.Title} - {post.User?.Username} - {post.Published_On} - {post.Blog!.Name} - {post.Blog.Url}");
        Console.WriteLine("");
    }

}

Console.WriteLine();
Console.WriteLine("///////////////////////////////");
Console.WriteLine();
Console.WriteLine();

var dbUsers = db.Users;
Console.WriteLine("Users in database");
Console.WriteLine("");
foreach (var user in dbUsers)
{
    Console.WriteLine("-----------------------------------------");
    Console.WriteLine();
    Console.WriteLine("\t" + user.Username);
   // Console.WriteLine($"{user.Username}");
    foreach (var post in user.Posts)
    {
        Console.WriteLine();
        Console.WriteLine($"{post.Title} - {post.Blog?.Name}");
        Console.WriteLine("");
    }
}

Console.WriteLine("");