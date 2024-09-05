using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentBloggAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class nyStudentbLoggDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdminUser",
                table: "Comments",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdminUser",
                table: "Comments");
        }
    }
}
