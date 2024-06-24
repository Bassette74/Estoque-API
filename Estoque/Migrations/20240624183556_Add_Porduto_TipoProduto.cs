using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estoque.Migrations
{
    /// <inheritdoc />
    public partial class Add_Porduto_TipoProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoProduto",
                table: "Produtos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoProduto",
                table: "Produtos");
        }
    }
}
