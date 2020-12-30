using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class IntroducedFullyDefinedRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assigns_ProjectManagers_ProjectManagerId",
                table: "Assigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Assigns_Tasks_TaskId",
                table: "Assigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Oversees_ProjectManagers_ProjectManagerId",
                table: "Oversees");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectManagers_Bosses_BossId",
                table: "ProjectManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Projects_ProjectId",
                table: "Steps");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Steps_StepId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "StepId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Steps",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BossId",
                table: "ProjectManagers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectManagerId",
                table: "Oversees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "Assigns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectManagerId",
                table: "Assigns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assigns_ProjectManagers_ProjectManagerId",
                table: "Assigns",
                column: "ProjectManagerId",
                principalTable: "ProjectManagers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assigns_Tasks_TaskId",
                table: "Assigns",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Oversees_ProjectManagers_ProjectManagerId",
                table: "Oversees",
                column: "ProjectManagerId",
                principalTable: "ProjectManagers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectManagers_Bosses_BossId",
                table: "ProjectManagers",
                column: "BossId",
                principalTable: "Bosses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Projects_ProjectId",
                table: "Steps",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Steps_StepId",
                table: "Tasks",
                column: "StepId",
                principalTable: "Steps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assigns_ProjectManagers_ProjectManagerId",
                table: "Assigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Assigns_Tasks_TaskId",
                table: "Assigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Oversees_ProjectManagers_ProjectManagerId",
                table: "Oversees");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectManagers_Bosses_BossId",
                table: "ProjectManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Projects_ProjectId",
                table: "Steps");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Steps_StepId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "StepId",
                table: "Tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Steps",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BossId",
                table: "ProjectManagers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectManagerId",
                table: "Oversees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "Assigns",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectManagerId",
                table: "Assigns",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Assigns_ProjectManagers_ProjectManagerId",
                table: "Assigns",
                column: "ProjectManagerId",
                principalTable: "ProjectManagers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assigns_Tasks_TaskId",
                table: "Assigns",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Oversees_ProjectManagers_ProjectManagerId",
                table: "Oversees",
                column: "ProjectManagerId",
                principalTable: "ProjectManagers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectManagers_Bosses_BossId",
                table: "ProjectManagers",
                column: "BossId",
                principalTable: "Bosses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Projects_ProjectId",
                table: "Steps",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Steps_StepId",
                table: "Tasks",
                column: "StepId",
                principalTable: "Steps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
