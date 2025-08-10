using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RuedasYPatas.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRazasTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RazaId",
                table: "Mascotas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Razas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Razas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_RazaId",
                table: "Mascotas",
                column: "RazaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mascotas_Razas_RazaId",
                table: "Mascotas",
                column: "RazaId",
                principalTable: "Razas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mascotas_Razas_RazaId",
                table: "Mascotas");

            migrationBuilder.DropTable(
                name: "Razas");

            migrationBuilder.DropIndex(
                name: "IX_Mascotas_RazaId",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "RazaId",
                table: "Mascotas");
        }
    }
}
