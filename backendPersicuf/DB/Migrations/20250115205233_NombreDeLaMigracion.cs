using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class NombreDeLaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prenda_Imagen_ImagenID",
                table: "Prenda");

            migrationBuilder.AlterColumn<int>(
                name: "ImagenID",
                table: "Prenda",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Prenda_Imagen_ImagenID",
                table: "Prenda",
                column: "ImagenID",
                principalTable: "Imagen",
                principalColumn: "ImagenID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prenda_Imagen_ImagenID",
                table: "Prenda");

            migrationBuilder.AlterColumn<int>(
                name: "ImagenID",
                table: "Prenda",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prenda_Imagen_ImagenID",
                table: "Prenda",
                column: "ImagenID",
                principalTable: "Imagen",
                principalColumn: "ImagenID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
