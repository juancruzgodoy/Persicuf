using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class DomicilioCorregido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Convertir valores no numéricos en NULL para evitar errores en la conversión
            migrationBuilder.Sql("UPDATE \"Domicilio\" SET \"Piso\" = NULL WHERE \"Piso\" !~ '^[0-9]+$';");

            // Cambiar el tipo de dato de TEXT a INTEGER con conversión explícita
            migrationBuilder.Sql("ALTER TABLE \"Domicilio\" ALTER COLUMN \"Piso\" TYPE integer USING \"Piso\"::integer;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revertir el cambio de tipo de INTEGER a TEXT
            migrationBuilder.Sql("ALTER TABLE \"Domicilio\" ALTER COLUMN \"Piso\" TYPE text USING \"Piso\"::text;");
        }
    }
}
