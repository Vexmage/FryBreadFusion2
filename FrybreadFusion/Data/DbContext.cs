using FrybreadFusion.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;


namespace FrybreadFusion.Data
{
    // Set to inherit from IdentityDbContext instead of DbContext
    public class MyDatabase : IdentityDbContext<AppUser>
    {
        public MyDatabase(DbContextOptions<MyDatabase> options)
            : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }


        // Seed data for BlogPosts
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BlogPost>().HasData(
                new BlogPost
                {
                    Id = 1,
                    Title = "Welcome to My Indigenous Cooking Adventures!",
                    Text = "I'm thrilled to embark on a flavorful journey, exploring the rich diversity of Indigenous cuisine! " +
                           "This space will not only be a testament to the culinary delights but also a place for us to delve into " +
                           "the profound history, philosophy, and stories of Indigenous cultures. Stay tuned for delectable recipes, " +
                           "insightful articles, and engaging tales that will ignite our senses and deepen our appreciation for these " +
                           "vibrant traditions.",
                    Name = "Admin",
                    DatePosted = new DateTime(2023, 11, 13)
                },
                new BlogPost
                {
                    Id = 2,
                    Title = "The Delectable World of Navajo Indian Tacos",
                    Text = "Navajo Indian Tacos are more than just a sumptuous feast; they represent a fusion of cultures and history. " +
                           "Originating from the Navajo people, these tacos are a beloved contemporary representation of Native American resilience " +
                           "and creativity. Typically made with frybread, a symbol of the pain and perseverance of the Navajo during their forced " +
                           "relocation, these tacos are topped with a variety of ingredients. Here's how you can make your own Navajo taco:  " +
                           "start with 3 cups of all - purpose flour, 1 tablespoon of baking powder, and a pinch of salt. Mix these dry ingredients" +
                           "before slowly adding warm water to form a dough." +
                           "Knead until smooth, then let it rest. Meanwhile, prepare your taco toppings: cook ground beef with " +
                           "taco seasoning, chop fresh lettuce and tomatoes, and grate some cheddar cheese. When ready to cook, shape the dough " +
                           "into small discs and fry until golden brown on both sides. Assemble your tacos by piling the toppings onto the frybread, " +
                           "and enjoy a delicious meal that's both a nod to tradition and a favorite modern comfort food.",
                    Name = "Sage Bearheart",
                    DatePosted = new DateTime(2023, 11, 14)
                }
            );

            // Seed data for Ratings
            modelBuilder.Entity<Rating>().ToTable("Rating");

            // Seed Data for Comments
            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    Id = 1,
                    BlogPostId = 1, // Assuming a foreign key to BlogPost
                    UserComment = "This is a great start to a cooking journey!",
                    UserName = "Foodie42",
                    DatePosted = new DateTime(2023, 1, 1)
                },
                new Comment
                {
                    Id = 2,
                    BlogPostId = 1,
                    UserComment = "Looking forward to seeing more recipes.",
                    UserName = "CookingEnthusiast",
                    DatePosted = new DateTime(2023, 1, 3)
                },
                new Comment
                {
                    Id = 3,
                    BlogPostId = 2,
                    UserComment = "Navajo tacos are the best!",
                    UserName = "Foodie42",
                    DatePosted = new DateTime(2023, 1, 5)
                }
            );

            // Seed data for Replies
            modelBuilder.Entity<Reply>().HasData(
                new Reply
                {
                    Id = 1,
                    CommentId = 1, // Make sure this matches an existing comment's ID
                    Text = "I completely agree with your points!",
                    UserName = "ReplyUser1", // Ensure this is provided
                    DatePosted = new DateTime(2023, 1, 2)
                },
                new Reply
                {
                    Id = 2,
                    CommentId = 2, // Ensure this matches an existing comment's ID
                    Text = "This is a test post!",
                    UserName = "ReplyUser2", // Ensure this is provided
                    DatePosted = new DateTime(2023, 1, 4)
                },
                new Reply
                {
                    Id = 3,
                    CommentId = 3, // Ensure this matches an existing comment's ID
                    Text = "What an insightful post!",
                    UserName = "ReplyUser3", // Ensure this is provided
                    DatePosted = new DateTime(2023, 1, 6)
                }
            );


            modelBuilder.Entity<Comment>()
    .HasMany(c => c.Replies)
    .WithOne(r => r.Comment)
    .HasForeignKey(r => r.CommentId)
    .OnDelete(DeleteBehavior.Cascade);
        }   
    }



}
