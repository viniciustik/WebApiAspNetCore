using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class alter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Documento",
                table: "tb_client",
                newName: "Document");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Document",
                table: "tb_client",
                newName: "Documento");
        }
    }
}
