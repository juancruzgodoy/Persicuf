using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class ImgURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagenData",
                table: "Imagen");

            migrationBuilder.AddColumn<string>(
                name: "ImagenPath",
                table: "Imagen",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagenPath",
                table: "Imagen");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImagenData",
                table: "Imagen",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
