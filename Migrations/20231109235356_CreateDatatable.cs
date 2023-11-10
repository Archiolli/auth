using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apibetter.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatatable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Extratos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumeroExtrato = table.Column<double>(type: "REAL", nullable: false),
                    UserEmail = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extratos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    Saldo = table.Column<double>(type: "REAL", nullable: false),
                    ExtratoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Extratos_ExtratoId",
                        column: x => x.ExtratoId,
                        principalTable: "Extratos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ExtratoId",
                table: "Users",
                column: "ExtratoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Extratos");
        }
    }
}
