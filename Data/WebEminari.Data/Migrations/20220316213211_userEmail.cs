using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebEminari.Data.Migrations
{
    public partial class userEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "UserBookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 16, 21, 32, 11, 211, DateTimeKind.Utc).AddTicks(8674));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "UserBookings");

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 16, 18, 53, 33, 890, DateTimeKind.Utc).AddTicks(302));
        }
    }
}
