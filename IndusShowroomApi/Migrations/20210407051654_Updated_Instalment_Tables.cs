using Microsoft.EntityFrameworkCore.Migrations;

namespace IndusShowroomApi.Migrations
{
    public partial class Updated_Instalment_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ACC_ID",
                table: "Instalment_Master");

            migrationBuilder.AlterColumn<int>(
                name: "TM_ID",
                table: "Instalment_Details",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IN_ID",
                table: "Instalment_Details",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ACC_ID",
                table: "Instalment_Master",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "TM_ID",
                table: "Instalment_Details",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "IN_ID",
                table: "Instalment_Details",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
