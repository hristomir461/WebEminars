﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace WebEminari.Data.Migrations
{
    public partial class ref3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedByUserFirstName",
                table: "WebEminars");

            migrationBuilder.DropColumn(
                name: "AddedByUserLastName",
                table: "WebEminars");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddedByUserFirstName",
                table: "WebEminars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddedByUserLastName",
                table: "WebEminars",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
