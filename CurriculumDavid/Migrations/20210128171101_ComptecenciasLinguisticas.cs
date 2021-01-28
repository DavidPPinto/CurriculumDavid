using Microsoft.EntityFrameworkCore.Migrations;

namespace CurriculumDavid.Migrations
{
    public partial class ComptecenciasLinguisticas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComLing",
                columns: table => new
                {
                    CompetenciasId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lingua = table.Column<string>(maxLength: 15, nullable: false),
                    CompreensaoOral = table.Column<string>(maxLength: 4, nullable: false),
                    Leitura = table.Column<string>(maxLength: 4, nullable: false),
                    ProducaoOral = table.Column<string>(maxLength: 4, nullable: false),
                    InteracaoOral = table.Column<string>(maxLength: 4, nullable: false),
                    Escrita = table.Column<string>(maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComLing", x => x.CompetenciasId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComLing");
        }
    }
}
