using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace teknofest.Migrations
{
    /// <inheritdoc />
    public partial class odanumara : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OdaNumarasi",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OdaNumarasi",
                table: "AspNetUsers");
        }
    }
}
