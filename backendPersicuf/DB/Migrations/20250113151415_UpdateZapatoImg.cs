using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class UpdateZapatoImg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Zapato",
                table: "Zapato");

            migrationBuilder.DropIndex(
                name: "IX_Zapato_PrendaID",
                table: "Zapato");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Remera",
                table: "Remera");

            migrationBuilder.DropIndex(
                name: "IX_Remera_PrendaID",
                table: "Remera");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pantalon",
                table: "Pantalon");

            migrationBuilder.DropIndex(
                name: "IX_Pantalon_PrendaID",
                table: "Pantalon");

            migrationBuilder.DropColumn(
                name: "ZapatoID",
                table: "Zapato");

            migrationBuilder.DropColumn(
                name: "RemeraID",
                table: "Remera");

            migrationBuilder.DropColumn(
                name: "PantalonID",
                table: "Pantalon");

            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Imagen");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImagenData",
                table: "Imagen",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zapato",
                table: "Zapato",
                column: "PrendaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Remera",
                table: "Remera",
                column: "PrendaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pantalon",
                table: "Pantalon",
                column: "PrendaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Zapato",
                table: "Zapato");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Remera",
                table: "Remera");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pantalon",
                table: "Pantalon");

            migrationBuilder.DropColumn(
                name: "ImagenData",
                table: "Imagen");

            migrationBuilder.AddColumn<int>(
                name: "ZapatoID",
                table: "Zapato",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "RemeraID",
                table: "Remera",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "PantalonID",
                table: "Pantalon",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Imagen",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zapato",
                table: "Zapato",
                column: "ZapatoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Remera",
                table: "Remera",
                column: "RemeraID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pantalon",
                table: "Pantalon",
                column: "PantalonID");

            migrationBuilder.CreateIndex(
                name: "IX_Zapato_PrendaID",
                table: "Zapato",
                column: "PrendaID");

            migrationBuilder.CreateIndex(
                name: "IX_Remera_PrendaID",
                table: "Remera",
                column: "PrendaID");

            migrationBuilder.CreateIndex(
                name: "IX_Pantalon_PrendaID",
                table: "Pantalon",
                column: "PrendaID");
        }
    }
}
