using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracker.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BugId",
                table: "comments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.AlterColumn<string>(
        //        name: "BugId",
        //        table: "comments",
        //        type: "nvarchar(max)",
        //        nullable: false,
        //        defaultValue: "",
        //        oldClrType: typeof(string),
        //        oldType: "nvarchar(max)",
        //        oldNullable: true);
        //}
    }
}
