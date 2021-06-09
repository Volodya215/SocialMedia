using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork_DAL.Migrations
{
    public partial class AddBlogLastModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastModify",
                table: "Chats",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModify",
                table: "Chats");
        }
    }
}
