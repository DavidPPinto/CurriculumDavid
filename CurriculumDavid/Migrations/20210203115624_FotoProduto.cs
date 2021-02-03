using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CurriculumDavid.Migrations
{
    public partial class FotoProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "DadosPessoais",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "DadosPessoais");
        }
    }
}
