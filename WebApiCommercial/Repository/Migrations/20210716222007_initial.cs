using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cpf = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CellPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Complement = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    NameState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    CnhRecord = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorporateName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_descriptionFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameProduct = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descriptionProduct = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    valueProduct = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    groupItems = table.Column<int>(type: "int", nullable: false),
                    idCompany = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_descriptionFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_descriptionFiles_tb_company_idCompany",
                        column: x => x.idCompany,
                        principalTable: "tb_company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCompany = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_user_tb_company_IdCompany",
                        column: x => x.IdCompany,
                        principalTable: "tb_company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_file",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Files = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IdDescriptionFiles = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_file", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_file_tb_descriptionFiles_IdDescriptionFiles",
                        column: x => x.IdDescriptionFiles,
                        principalTable: "tb_descriptionFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tb_company",
                columns: new[] { "Id", "CorporateName" },
                values: new object[] { 1, "Empresa Padrão" });

            migrationBuilder.InsertData(
                table: "tb_user",
                columns: new[] { "Id", "BirthDate", "Email", "IdCompany", "Name", "Password", "Role" },
                values: new object[] { 1, new DateTime(1983, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@padrao.com.br", 1, "Admin", "Admin", null });

            migrationBuilder.CreateIndex(
                name: "IX_tb_descriptionFiles_idCompany",
                table: "tb_descriptionFiles",
                column: "idCompany");

            migrationBuilder.CreateIndex(
                name: "IX_tb_file_IdDescriptionFiles",
                table: "tb_file",
                column: "IdDescriptionFiles");

            migrationBuilder.CreateIndex(
                name: "IX_tb_user_IdCompany",
                table: "tb_user",
                column: "IdCompany");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_client");

            migrationBuilder.DropTable(
                name: "tb_file");

            migrationBuilder.DropTable(
                name: "tb_user");

            migrationBuilder.DropTable(
                name: "tb_descriptionFiles");

            migrationBuilder.DropTable(
                name: "tb_company");
        }
    }
}
