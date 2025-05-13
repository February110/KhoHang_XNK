using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KhoHang_XNK.Migrations
{
    /// <inheritdoc />
    public partial class thanhtoandonxuat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "trangthaithanhtoan",
                table: "DonXuatHangs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "trangthaithanhtoan",
                table: "DonXuatHangs");
        }
    }
}
