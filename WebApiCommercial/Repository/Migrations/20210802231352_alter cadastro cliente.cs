using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class altercadastrocliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "tb_client");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "tb_client");

            migrationBuilder.RenameColumn(
                name: "CnhRecord",
                table: "tb_client",
                newName: "Documento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Documento",
                table: "tb_client",
                newName: "CnhRecord");

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "tb_client",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "tb_client",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
