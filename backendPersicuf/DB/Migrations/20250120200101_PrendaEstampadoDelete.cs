using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class PrendaEstampadoDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrendaEstampado");

            migrationBuilder.AddColumn<int>(
                name: "EstampadoID",
                table: "Prenda",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UbicacionID",
                table: "Imagen",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prenda_EstampadoID",
                table: "Prenda",
                column: "EstampadoID");

            migrationBuilder.CreateIndex(
                name: "IX_Imagen_UbicacionID",
                table: "Imagen",
                column: "UbicacionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Imagen_Ubicacion_UbicacionID",
                table: "Imagen",
                column: "UbicacionID",
                principalTable: "Ubicacion",
                principalColumn: "UbicacionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Prenda_Imagen_EstampadoID",
                table: "Prenda",
                column: "EstampadoID",
                principalTable: "Imagen",
                principalColumn: "ImagenID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Imagen_Ubicacion_UbicacionID",
                table: "Imagen");

            migrationBuilder.DropForeignKey(
                name: "FK_Prenda_Imagen_EstampadoID",
                table: "Prenda");

            migrationBuilder.DropIndex(
                name: "IX_Prenda_EstampadoID",
                table: "Prenda");

            migrationBuilder.DropIndex(
                name: "IX_Imagen_UbicacionID",
                table: "Imagen");

            migrationBuilder.DropColumn(
                name: "EstampadoID",
                table: "Prenda");

            migrationBuilder.DropColumn(
                name: "UbicacionID",
                table: "Imagen");

            migrationBuilder.CreateTable(
                name: "PrendaEstampado",
                columns: table => new
                {
                    PEID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImagenID = table.Column<int>(type: "integer", nullable: false),
                    PrendaID = table.Column<int>(type: "integer", nullable: false),
                    UbicacionID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrendaEstampado", x => x.PEID);
                    table.ForeignKey(
                        name: "FK_PrendaEstampado_Imagen_ImagenID",
                        column: x => x.ImagenID,
                        principalTable: "Imagen",
                        principalColumn: "ImagenID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrendaEstampado_Prenda_PrendaID",
                        column: x => x.PrendaID,
                        principalTable: "Prenda",
                        principalColumn: "PrendaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrendaEstampado_Ubicacion_UbicacionID",
                        column: x => x.UbicacionID,
                        principalTable: "Ubicacion",
                        principalColumn: "UbicacionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrendaEstampado_ImagenID",
                table: "PrendaEstampado",
                column: "ImagenID");

            migrationBuilder.CreateIndex(
                name: "IX_PrendaEstampado_PrendaID",
                table: "PrendaEstampado",
                column: "PrendaID");

            migrationBuilder.CreateIndex(
                name: "IX_PrendaEstampado_UbicacionID",
                table: "PrendaEstampado",
                column: "UbicacionID");
        }
    }
}
