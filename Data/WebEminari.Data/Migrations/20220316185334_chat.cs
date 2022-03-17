using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebEminari.Data.Migrations
{
    public partial class chat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "Id", "CreatedOn", "ModifiedOn", "Name" },
                values: new object[] { 1, new DateTime(2022, 3, 16, 18, 53, 33, 890, DateTimeKind.Utc).AddTicks(302), null, "Main" });

            migrationBuilder.InsertData(
                table: "ChatApplicationUsers",
                columns: new[] { "ChatId", "UserId" },
                values: new object[] { 1, "ec6406bf-06bd-414a-8dd2-c3418336f631" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChatApplicationUsers",
                keyColumns: new[] { "ChatId", "UserId" },
                keyValues: new object[] { 1, "ec6406bf-06bd-414a-8dd2-c3418336f631" });

            migrationBuilder.DeleteData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
