using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    ColorID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ColorNombre = table.Column<string>(type: "text", nullable: false),
                    CodigoHexa = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.ColorID);
                });

            migrationBuilder.CreateTable(
                name: "CorteCuello",
                columns: table => new
                {
                    CCID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorteCuello", x => x.CCID);
                });

            migrationBuilder.CreateTable(
                name: "Imagen",
                columns: table => new
                {
                    ImagenID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImgPath = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagen", x => x.ImagenID);
                });

            migrationBuilder.CreateTable(
                name: "Largo",
                columns: table => new
                {
                    LargoID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Largo", x => x.LargoID);
                });

            migrationBuilder.CreateTable(
                name: "Manga",
                columns: table => new
                {
                    MangaID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manga", x => x.MangaID);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    MaterialID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Precio = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.MaterialID);
                });

            migrationBuilder.CreateTable(
                name: "Permiso",
                columns: table => new
                {
                    PermisoID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permiso", x => x.PermisoID);
                });

            migrationBuilder.CreateTable(
                name: "Provincia",
                columns: table => new
                {
                    ProvinciaID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProvinciaNombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincia", x => x.ProvinciaID);
                });

            migrationBuilder.CreateTable(
                name: "Rubro",
                columns: table => new
                {
                    RubroID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubro", x => x.RubroID);
                });

            migrationBuilder.CreateTable(
                name: "TalleAlfabetico",
                columns: table => new
                {
                    TAID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalleAlfabetico", x => x.TAID);
                });

            migrationBuilder.CreateTable(
                name: "TalleNumerico",
                columns: table => new
                {
                    TNID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalleNumerico", x => x.TNID);
                });

            migrationBuilder.CreateTable(
                name: "Ubicacion",
                columns: table => new
                {
                    UbicacionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    PosX = table.Column<float>(type: "real", nullable: false),
                    PosY = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicacion", x => x.UbicacionID);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreUsuario = table.Column<string>(type: "text", nullable: false),
                    Contrasenia = table.Column<string>(type: "text", nullable: false),
                    Correo = table.Column<string>(type: "text", nullable: false),
                    PermisoID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioID);
                    table.ForeignKey(
                        name: "FK_Usuario_Permiso_PermisoID",
                        column: x => x.PermisoID,
                        principalTable: "Permiso",
                        principalColumn: "PermisoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Localidad",
                columns: table => new
                {
                    LocalidadID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    ProvinciaID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localidad", x => x.LocalidadID);
                    table.ForeignKey(
                        name: "FK_Localidad_Provincia_ProvinciaID",
                        column: x => x.ProvinciaID,
                        principalTable: "Provincia",
                        principalColumn: "ProvinciaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prenda",
                columns: table => new
                {
                    PrendaID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Precio = table.Column<float>(type: "real", nullable: false),
                    ColorID = table.Column<int>(type: "integer", nullable: false),
                    RubroID = table.Column<int>(type: "integer", nullable: false),
                    MaterialID = table.Column<int>(type: "integer", nullable: false),
                    UsuarioID = table.Column<int>(type: "integer", nullable: false),
                    ImagenID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prenda", x => x.PrendaID);
                    table.ForeignKey(
                        name: "FK_Prenda_Color_ColorID",
                        column: x => x.ColorID,
                        principalTable: "Color",
                        principalColumn: "ColorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prenda_Imagen_ImagenID",
                        column: x => x.ImagenID,
                        principalTable: "Imagen",
                        principalColumn: "ImagenID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prenda_Material_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Material",
                        principalColumn: "MaterialID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prenda_Rubro_RubroID",
                        column: x => x.RubroID,
                        principalTable: "Rubro",
                        principalColumn: "RubroID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prenda_Usuario_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Domicilio",
                columns: table => new
                {
                    DomicilioID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Calle = table.Column<string>(type: "text", nullable: false),
                    Altura = table.Column<int>(type: "integer", nullable: false),
                    Departamento = table.Column<string>(type: "text", nullable: false),
                    UsuarioID = table.Column<int>(type: "integer", nullable: false),
                    LocalidadID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domicilio", x => x.DomicilioID);
                    table.ForeignKey(
                        name: "FK_Domicilio_Localidad_LocalidadID",
                        column: x => x.LocalidadID,
                        principalTable: "Localidad",
                        principalColumn: "LocalidadID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Domicilio_Usuario_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Campera",
                columns: table => new
                {
                    CamperaID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrendaID = table.Column<int>(type: "integer", nullable: false),
                    TAID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campera", x => x.CamperaID);
                    table.ForeignKey(
                        name: "FK_Campera_Prenda_PrendaID",
                        column: x => x.PrendaID,
                        principalTable: "Prenda",
                        principalColumn: "PrendaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Campera_TalleAlfabetico_TAID",
                        column: x => x.TAID,
                        principalTable: "TalleAlfabetico",
                        principalColumn: "TAID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pantalon",
                columns: table => new
                {
                    PantalonID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrendaID = table.Column<int>(type: "integer", nullable: false),
                    LargoID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pantalon", x => x.PantalonID);
                    table.ForeignKey(
                        name: "FK_Pantalon_Largo_LargoID",
                        column: x => x.LargoID,
                        principalTable: "Largo",
                        principalColumn: "LargoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pantalon_Prenda_PrendaID",
                        column: x => x.PrendaID,
                        principalTable: "Prenda",
                        principalColumn: "PrendaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrendaEstampado",
                columns: table => new
                {
                    PEID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrendaID = table.Column<int>(type: "integer", nullable: false),
                    ImagenID = table.Column<int>(type: "integer", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Remera",
                columns: table => new
                {
                    RemeraID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrendaID = table.Column<int>(type: "integer", nullable: false),
                    MangaID = table.Column<int>(type: "integer", nullable: false),
                    CCID = table.Column<int>(type: "integer", nullable: false),
                    TAID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remera", x => x.RemeraID);
                    table.ForeignKey(
                        name: "FK_Remera_CorteCuello_CCID",
                        column: x => x.CCID,
                        principalTable: "CorteCuello",
                        principalColumn: "CCID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Remera_Manga_MangaID",
                        column: x => x.MangaID,
                        principalTable: "Manga",
                        principalColumn: "MangaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Remera_Prenda_PrendaID",
                        column: x => x.PrendaID,
                        principalTable: "Prenda",
                        principalColumn: "PrendaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Remera_TalleAlfabetico_TAID",
                        column: x => x.TAID,
                        principalTable: "TalleAlfabetico",
                        principalColumn: "TAID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zapato",
                columns: table => new
                {
                    ZapatoID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrendaID = table.Column<int>(type: "integer", nullable: false),
                    PuntaMetalica = table.Column<bool>(type: "boolean", nullable: false),
                    TNID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zapato", x => x.ZapatoID);
                    table.ForeignKey(
                        name: "FK_Zapato_Prenda_PrendaID",
                        column: x => x.PrendaID,
                        principalTable: "Prenda",
                        principalColumn: "PrendaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zapato_TalleNumerico_TNID",
                        column: x => x.TNID,
                        principalTable: "TalleNumerico",
                        principalColumn: "TNID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    PedidoID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrecioTotal = table.Column<float>(type: "real", nullable: false),
                    DomicilioID = table.Column<int>(type: "integer", nullable: false),
                    UsuarioID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.PedidoID);
                    table.ForeignKey(
                        name: "FK_Pedido_Domicilio_DomicilioID",
                        column: x => x.DomicilioID,
                        principalTable: "Domicilio",
                        principalColumn: "DomicilioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedido_Usuario_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoPrenda",
                columns: table => new
                {
                    PPID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    PrendaID = table.Column<int>(type: "integer", nullable: false),
                    PedidoID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoPrenda", x => x.PPID);
                    table.ForeignKey(
                        name: "FK_PedidoPrenda_Pedido_PedidoID",
                        column: x => x.PedidoID,
                        principalTable: "Pedido",
                        principalColumn: "PedidoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoPrenda_Prenda_PrendaID",
                        column: x => x.PrendaID,
                        principalTable: "Prenda",
                        principalColumn: "PrendaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campera_PrendaID",
                table: "Campera",
                column: "PrendaID");

            migrationBuilder.CreateIndex(
                name: "IX_Campera_TAID",
                table: "Campera",
                column: "TAID");

            migrationBuilder.CreateIndex(
                name: "IX_Domicilio_LocalidadID",
                table: "Domicilio",
                column: "LocalidadID");

            migrationBuilder.CreateIndex(
                name: "IX_Domicilio_UsuarioID",
                table: "Domicilio",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Localidad_ProvinciaID",
                table: "Localidad",
                column: "ProvinciaID");

            migrationBuilder.CreateIndex(
                name: "IX_Pantalon_LargoID",
                table: "Pantalon",
                column: "LargoID");

            migrationBuilder.CreateIndex(
                name: "IX_Pantalon_PrendaID",
                table: "Pantalon",
                column: "PrendaID");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_DomicilioID",
                table: "Pedido",
                column: "DomicilioID");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_UsuarioID",
                table: "Pedido",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoPrenda_PedidoID",
                table: "PedidoPrenda",
                column: "PedidoID");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoPrenda_PrendaID",
                table: "PedidoPrenda",
                column: "PrendaID");

            migrationBuilder.CreateIndex(
                name: "IX_Prenda_ColorID",
                table: "Prenda",
                column: "ColorID");

            migrationBuilder.CreateIndex(
                name: "IX_Prenda_ImagenID",
                table: "Prenda",
                column: "ImagenID");

            migrationBuilder.CreateIndex(
                name: "IX_Prenda_MaterialID",
                table: "Prenda",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_Prenda_RubroID",
                table: "Prenda",
                column: "RubroID");

            migrationBuilder.CreateIndex(
                name: "IX_Prenda_UsuarioID",
                table: "Prenda",
                column: "UsuarioID");

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

            migrationBuilder.CreateIndex(
                name: "IX_Remera_CCID",
                table: "Remera",
                column: "CCID");

            migrationBuilder.CreateIndex(
                name: "IX_Remera_MangaID",
                table: "Remera",
                column: "MangaID");

            migrationBuilder.CreateIndex(
                name: "IX_Remera_PrendaID",
                table: "Remera",
                column: "PrendaID");

            migrationBuilder.CreateIndex(
                name: "IX_Remera_TAID",
                table: "Remera",
                column: "TAID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PermisoID",
                table: "Usuario",
                column: "PermisoID");

            migrationBuilder.CreateIndex(
                name: "IX_Zapato_PrendaID",
                table: "Zapato",
                column: "PrendaID");

            migrationBuilder.CreateIndex(
                name: "IX_Zapato_TNID",
                table: "Zapato",
                column: "TNID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Campera");

            migrationBuilder.DropTable(
                name: "Pantalon");

            migrationBuilder.DropTable(
                name: "PedidoPrenda");

            migrationBuilder.DropTable(
                name: "PrendaEstampado");

            migrationBuilder.DropTable(
                name: "Remera");

            migrationBuilder.DropTable(
                name: "Zapato");

            migrationBuilder.DropTable(
                name: "Largo");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Ubicacion");

            migrationBuilder.DropTable(
                name: "CorteCuello");

            migrationBuilder.DropTable(
                name: "Manga");

            migrationBuilder.DropTable(
                name: "TalleAlfabetico");

            migrationBuilder.DropTable(
                name: "Prenda");

            migrationBuilder.DropTable(
                name: "TalleNumerico");

            migrationBuilder.DropTable(
                name: "Domicilio");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Imagen");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Rubro");

            migrationBuilder.DropTable(
                name: "Localidad");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Provincia");

            migrationBuilder.DropTable(
                name: "Permiso");
        }
    }
}
