using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddedDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assign_ProjectManager_ProjectManagerId",
                table: "Assign");

            migrationBuilder.DropForeignKey(
                name: "FK_Assign_Tasks_TaskId",
                table: "Assign");

            migrationBuilder.DropForeignKey(
                name: "FK_Oversee_ProjectManager_ProjectManagerId",
                table: "Oversee");

            migrationBuilder.DropForeignKey(
                name: "FK_Oversee_Tasks_TaskId",
                table: "Oversee");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectManager_Boss_BossId",
                table: "ProjectManager");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectManager",
                table: "ProjectManager");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Oversee",
                table: "Oversee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Boss",
                table: "Boss");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assign",
                table: "Assign");

            migrationBuilder.RenameTable(
                name: "ProjectManager",
                newName: "ProjectManagers");

            migrationBuilder.RenameTable(
                name: "Oversee",
                newName: "Oversees");

            migrationBuilder.RenameTable(
                name: "Boss",
                newName: "Bosses");

            migrationBuilder.RenameTable(
                name: "Assign",
                newName: "Assigns");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectManager_BossId",
                table: "ProjectManagers",
                newName: "IX_ProjectManagers_BossId");

            migrationBuilder.RenameIndex(
                name: "IX_Oversee_TaskId",
                table: "Oversees",
                newName: "IX_Oversees_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Oversee_ProjectManagerId",
                table: "Oversees",
                newName: "IX_Oversees_ProjectManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Assign_TaskId",
                table: "Assigns",
                newName: "IX_Assigns_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Assign_ProjectManagerId",
                table: "Assigns",
                newName: "IX_Assigns_ProjectManagerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectManagers",
                table: "ProjectManagers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Oversees",
                table: "Oversees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bosses",
                table: "Bosses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assigns",
                table: "Assigns",
                column: "Id");

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
                name: "FK_Oversees_Tasks_TaskId",
                table: "Oversees",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectManagers_Bosses_BossId",
                table: "ProjectManagers",
                column: "BossId",
                principalTable: "Bosses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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
                name: "FK_Oversees_Tasks_TaskId",
                table: "Oversees");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectManagers_Bosses_BossId",
                table: "ProjectManagers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectManagers",
                table: "ProjectManagers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Oversees",
                table: "Oversees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bosses",
                table: "Bosses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assigns",
                table: "Assigns");

            migrationBuilder.RenameTable(
                name: "ProjectManagers",
                newName: "ProjectManager");

            migrationBuilder.RenameTable(
                name: "Oversees",
                newName: "Oversee");

            migrationBuilder.RenameTable(
                name: "Bosses",
                newName: "Boss");

            migrationBuilder.RenameTable(
                name: "Assigns",
                newName: "Assign");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectManagers_BossId",
                table: "ProjectManager",
                newName: "IX_ProjectManager_BossId");

            migrationBuilder.RenameIndex(
                name: "IX_Oversees_TaskId",
                table: "Oversee",
                newName: "IX_Oversee_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Oversees_ProjectManagerId",
                table: "Oversee",
                newName: "IX_Oversee_ProjectManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Assigns_TaskId",
                table: "Assign",
                newName: "IX_Assign_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Assigns_ProjectManagerId",
                table: "Assign",
                newName: "IX_Assign_ProjectManagerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectManager",
                table: "ProjectManager",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Oversee",
                table: "Oversee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boss",
                table: "Boss",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assign",
                table: "Assign",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assign_ProjectManager_ProjectManagerId",
                table: "Assign",
                column: "ProjectManagerId",
                principalTable: "ProjectManager",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assign_Tasks_TaskId",
                table: "Assign",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Oversee_ProjectManager_ProjectManagerId",
                table: "Oversee",
                column: "ProjectManagerId",
                principalTable: "ProjectManager",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Oversee_Tasks_TaskId",
                table: "Oversee",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectManager_Boss_BossId",
                table: "ProjectManager",
                column: "BossId",
                principalTable: "Boss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
