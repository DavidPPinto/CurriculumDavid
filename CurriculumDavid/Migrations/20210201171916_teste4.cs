using Microsoft.EntityFrameworkCore.Migrations;

namespace CurriculumDavid.Migrations
{
    public partial class teste4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DadosPessoaisId1",
                table: "DadosPessoais",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DadosPessoais_DadosPessoaisId1",
                table: "DadosPessoais",
                column: "DadosPessoaisId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DadosPessoais_DadosPessoais_DadosPessoaisId1",
                table: "DadosPessoais",
                column: "DadosPessoaisId1",
                principalTable: "DadosPessoais",
                principalColumn: "DadosPessoaisId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DadosPessoais_DadosPessoais_DadosPessoaisId1",
                table: "DadosPessoais");

            migrationBuilder.DropIndex(
                name: "IX_DadosPessoais_DadosPessoaisId1",
                table: "DadosPessoais");

            migrationBuilder.DropColumn(
                name: "DadosPessoaisId1",
                table: "DadosPessoais");
        }
    }
}
