using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MVCProject.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DisciplineId = table.Column<int>(nullable: false),
                    FinalMark = table.Column<string>(nullable: true),
                    FirstMark = table.Column<double>(nullable: false),
                    FirstMarkUserId = table.Column<string>(nullable: true),
                    SecondMark = table.Column<double>(nullable: false),
                    SecondMarkUserId = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    ThirdMark = table.Column<double>(nullable: false),
                    ThirdMarkUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DisciplineId = table.Column<int>(nullable: false),
                    FinalMark = table.Column<string>(nullable: true),
                    FirstMark = table.Column<double>(nullable: false),
                    FirstMarkUserId = table.Column<string>(nullable: true),
                    SecondMark = table.Column<double>(nullable: false),
                    SecondMarkUserId = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    ThirdMark = table.Column<double>(nullable: false),
                    ThirdMarkUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                });
        }
    }
}
