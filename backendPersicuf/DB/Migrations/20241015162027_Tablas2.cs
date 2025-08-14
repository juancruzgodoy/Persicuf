using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class Tablas2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "TalleNumerico",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Prenda",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TAID",
                table: "Pantalon",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pantalon_TAID",
                table: "Pantalon",
                column: "TAID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pantalon_TalleAlfabetico_TAID",
                table: "Pantalon",
                column: "TAID",
                principalTable: "TalleAlfabetico",
                principalColumn: "TAID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pantalon_TalleAlfabetico_TAID",
                table: "Pantalon");

            migrationBuilder.DropIndex(
                name: "IX_Pantalon_TAID",
                table: "Pantalon");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Prenda");

            migrationBuilder.DropColumn(
                name: "TAID",
                table: "Pantalon");

            migrationBuilder.AlterColumn<int>(
                name: "Descripcion",
                table: "TalleNumerico",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
