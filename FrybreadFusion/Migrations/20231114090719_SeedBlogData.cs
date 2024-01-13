using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FrybreadFusion.Migrations
{
    /// <inheritdoc />
    public partial class SeedBlogData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_BlogPosts_BlogPostId",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "Rating");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_BlogPostId",
                table: "Rating",
                newName: "IX_Rating_BlogPostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rating",
                table: "Rating",
                column: "Id");

            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "DatePosted", "Name", "Text", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "I'm thrilled to embark on a flavorful journey, exploring the rich diversity of Indigenous cuisine! This space will not only be a testament to the culinary delights but also a place for us to delve into the profound history, philosophy, and stories of Indigenous cultures. Stay tuned for delectable recipes, insightful articles, and engaging tales that will ignite our senses and deepen our appreciation for these vibrant traditions.", "Welcome to My Indigenous Cooking Adventures!" },
                    { 2, new DateTime(2023, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sage Bearheart", "Navajo Indian Tacos are more than just a sumptuous feast; they represent a fusion of cultures and history. Originating from the Navajo people, these tacos are a beloved contemporary representation of Native American resilience and creativity. Typically made with frybread, a symbol of the pain and perseverance of the Navajo during their forced relocation, these tacos are topped with a variety of ingredients. Here's how you can make your own Navajo taco:  start with 3 cups of all - purpose flour, 1 tablespoon of baking powder, and a pinch of salt. Mix these dry ingredientsbefore slowly adding warm water to form a dough.Knead until smooth, then let it rest. Meanwhile, prepare your taco toppings: cook ground beef with taco seasoning, chop fresh lettuce and tomatoes, and grate some cheddar cheese. When ready to cook, shape the dough into small discs and fry until golden brown on both sides. Assemble your tacos by piling the toppings onto the frybread, and enjoy a delicious meal that's both a nod to tradition and a favorite modern comfort food.", "The Delectable World of Navajo Indian Tacos" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_BlogPosts_BlogPostId",
                table: "Rating",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_BlogPosts_BlogPostId",
                table: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rating",
                table: "Rating");

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "Rating",
                newName: "Ratings");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_BlogPostId",
                table: "Ratings",
                newName: "IX_Ratings_BlogPostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_BlogPosts_BlogPostId",
                table: "Ratings",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
