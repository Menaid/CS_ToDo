using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }
    public string DbPath { get; }

    public BloggingContext()
    {
        DbPath = "sys23-Menaid-Nocic.db";  // Tog bort rader för path som fanns i exemplet och döpte till detta för att spara det lokalt i projekt mappen.
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

// Har valt att inte ha med post_id i Blog för att om en användare vill göra flera posts i olika bloggar så sköts det i Post tabellen där varje inlägg av användaren i olika bloggar får en unik Id.
public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
    public string Name { get; set; }
    public List<Post> Posts { get; } = new();
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public DateOnly Published_On { get; set; }
    
    public int BlogId { get; set; }
    public int UserId { get; set; }
    
    public Blog Blog { get; set; }
    public User User { get; set; }
}

// Har valt att inte ha med post_id i Users för att om en användare vill göra flera posts så sköts det i Post tabellen där varje inlägg av användaren får en unik Id.
public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Post> Posts { get; } = new();
}
