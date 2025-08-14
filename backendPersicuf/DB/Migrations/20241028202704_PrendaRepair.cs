using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class PrendaRepair : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Campera",
                table: "Campera");

            migrationBuilder.DropIndex(
                name: "IX_Campera_PrendaID",
                table: "Campera");

            migrationBuilder.DropColumn(
                name: "CamperaID",
                table: "Campera");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Campera",
                table: "Campera",
                column: "PrendaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Campera",
                table: "Campera");

            migrationBuilder.AddColumn<int>(
                name: "CamperaID",
                table: "Campera",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Campera",
                table: "Campera",
                column: "CamperaID");

            migrationBuilder.CreateIndex(
                name: "IX_Campera_PrendaID",
                table: "Campera",
                column: "PrendaID");
        }
    }
}
