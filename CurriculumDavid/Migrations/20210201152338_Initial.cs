using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CurriculumDavid.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DadosPessoais",
                columns: table => new
                {
                    DadosPessoaisId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 300, nullable: false),
                    Morada = table.Column<string>(maxLength: 500, nullable: false),
                    Telefone = table.Column<string>(maxLength: 9, nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosPessoais", x => x.DadosPessoaisId);
                });

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
                    Escrita = table.Column<string>(maxLength: 4, nullable: false),
                    DadosPessoaisId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComLing", x => x.CompetenciasId);
                    table.ForeignKey(
                        name: "FK_ComLing_DadosPessoais_DadosPessoaisId",
                        column: x => x.DadosPessoaisId,
                        principalTable: "DadosPessoais",
                        principalColumn: "DadosPessoaisId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EduFor",
                columns: table => new
                {
                    EduForId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: false),
                    NomeFormacao = table.Column<string>(maxLength: 300, nullable: false),
                    EntFormadora = table.Column<string>(maxLength: 300, nullable: false),
                    DadosPessoaisId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduFor", x => x.EduForId);
                    table.ForeignKey(
                        name: "FK_EduFor_DadosPessoais_DadosPessoaisId",
                        column: x => x.DadosPessoaisId,
                        principalTable: "DadosPessoais",
                        principalColumn: "DadosPessoaisId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpProfissional",
                columns: table => new
                {
                    ExpProfissionalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: false),
                    NomeEmpresa = table.Column<string>(maxLength: 300, nullable: false),
                    Funcao = table.Column<string>(maxLength: 300, nullable: false),
                    DadosPessoaisId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpProfissional", x => x.ExpProfissionalId);
                    table.ForeignKey(
                        name: "FK_ExpProfissional_DadosPessoais_DadosPessoaisId",
                        column: x => x.DadosPessoaisId,
                        principalTable: "DadosPessoais",
                        principalColumn: "DadosPessoaisId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComLing_DadosPessoaisId",
                table: "ComLing",
                column: "DadosPessoaisId");

            migrationBuilder.CreateIndex(
                name: "IX_EduFor_DadosPessoaisId",
                table: "EduFor",
                column: "DadosPessoaisId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpProfissional_DadosPessoaisId",
                table: "ExpProfissional",
                column: "DadosPessoaisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComLing");

            migrationBuilder.DropTable(
                name: "EduFor");

            migrationBuilder.DropTable(
                name: "ExpProfissional");

            migrationBuilder.DropTable(
                name: "DadosPessoais");
        }
    }
}
