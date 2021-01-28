using Microsoft.EntityFrameworkCore.Migrations;

namespace CurriculumDavid.Migrations
{
    public partial class ExpProfisional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpProfissional",
                columns: table => new
                {
                    ExpProfissionalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInicio = table.Column<int>(nullable: false),
                    DataFim = table.Column<int>(nullable: false),
                    NomeEmpresa = table.Column<string>(maxLength: 300, nullable: false),
                    Funcao = table.Column<string>(maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpProfissional", x => x.ExpProfissionalId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpProfissional");
        }
    }
}
