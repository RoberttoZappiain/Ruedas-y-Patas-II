using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RuedasYPatas.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDomainModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreCompleto",
                table: "AspNetUsers",
                newName: "Nombre");

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoComentario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AutorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceptorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentarios_AspNetUsers_AutorId",
                        column: x => x.AutorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comentarios_AspNetUsers_ReceptorId",
                        column: x => x.ReceptorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Mascotas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Especie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Raza = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mascotas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mascotas_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ubicaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ciudad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hospedajes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostoPorNoche = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UbicacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospedajes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hospedajes_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hospedajes_Ubicaciones_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transportes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaLlegada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Capacidad = table.Column<int>(type: "int", nullable: false),
                    UsuarioConductorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UbicacionOrigenId = table.Column<int>(type: "int", nullable: false),
                    UbicacionDestinoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transportes_AspNetUsers_UsuarioConductorId",
                        column: x => x.UsuarioConductorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transportes_Ubicaciones_UbicacionDestinoId",
                        column: x => x.UbicacionDestinoId,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transportes_Ubicaciones_UbicacionOrigenId",
                        column: x => x.UbicacionOrigenId,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReservasHospedaje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoReserva = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MascotaId = table.Column<int>(type: "int", nullable: false),
                    HospedajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservasHospedaje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservasHospedaje_Hospedajes_HospedajeId",
                        column: x => x.HospedajeId,
                        principalTable: "Hospedajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservasHospedaje_Mascotas_MascotaId",
                        column: x => x.MascotaId,
                        principalTable: "Mascotas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesTransporte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstadoSolicitud = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MascotaId = table.Column<int>(type: "int", nullable: false),
                    TransporteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesTransporte", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudesTransporte_Mascotas_MascotaId",
                        column: x => x.MascotaId,
                        principalTable: "Mascotas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SolicitudesTransporte_Transportes_TransporteId",
                        column: x => x.TransporteId,
                        principalTable: "Transportes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_AutorId",
                table: "Comentarios",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_ReceptorId",
                table: "Comentarios",
                column: "ReceptorId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospedajes_UbicacionId",
                table: "Hospedajes",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospedajes_UsuarioId",
                table: "Hospedajes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_UsuarioId",
                table: "Mascotas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservasHospedaje_HospedajeId",
                table: "ReservasHospedaje",
                column: "HospedajeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservasHospedaje_MascotaId",
                table: "ReservasHospedaje",
                column: "MascotaId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesTransporte_MascotaId",
                table: "SolicitudesTransporte",
                column: "MascotaId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesTransporte_TransporteId",
                table: "SolicitudesTransporte",
                column: "TransporteId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportes_UbicacionDestinoId",
                table: "Transportes",
                column: "UbicacionDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportes_UbicacionOrigenId",
                table: "Transportes",
                column: "UbicacionOrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportes_UsuarioConductorId",
                table: "Transportes",
                column: "UsuarioConductorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "ReservasHospedaje");

            migrationBuilder.DropTable(
                name: "SolicitudesTransporte");

            migrationBuilder.DropTable(
                name: "Hospedajes");

            migrationBuilder.DropTable(
                name: "Mascotas");

            migrationBuilder.DropTable(
                name: "Transportes");

            migrationBuilder.DropTable(
                name: "Ubicaciones");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "AspNetUsers",
                newName: "NombreCompleto");
        }
    }
}
