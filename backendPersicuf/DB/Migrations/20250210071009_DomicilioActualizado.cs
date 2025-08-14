using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class DomicilioActualizado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Departamento",
                table: "Domicilio",
                newName: "Piso");

            migrationBuilder.RenameColumn(
                name: "Altura",
                table: "Domicilio",
                newName: "Numero");

            migrationBuilder.AddColumn<string>(
                name: "Depto",
                table: "Domicilio",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Domicilio",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Depto",
                table: "Domicilio");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Domicilio");

            migrationBuilder.RenameColumn(
                name: "Piso",
                table: "Domicilio",
                newName: "Departamento");

            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "Domicilio",
                newName: "Altura");
        }
    }
}
