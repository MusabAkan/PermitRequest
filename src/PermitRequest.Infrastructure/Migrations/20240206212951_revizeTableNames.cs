using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PermitRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class revizeTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequest_AdUsers_CreatedById",
                table: "LeaveRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AdUsers_UserId",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_CumulativeLeaveRequests_CumulativeLeaveRequestId",
                table: "Notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notification",
                table: "Notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaveRequest",
                table: "LeaveRequest");

            migrationBuilder.RenameTable(
                name: "Notification",
                newName: "Notifications");

            migrationBuilder.RenameTable(
                name: "LeaveRequest",
                newName: "LeaveRequests");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_UserId",
                table: "Notifications",
                newName: "IX_Notifications_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_CumulativeLeaveRequestId",
                table: "Notifications",
                newName: "IX_Notifications_CumulativeLeaveRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveRequest_CreatedById",
                table: "LeaveRequests",
                newName: "IX_LeaveRequests_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaveRequests",
                table: "LeaveRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_AdUsers_CreatedById",
                table: "LeaveRequests",
                column: "CreatedById",
                principalTable: "AdUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AdUsers_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "AdUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_CumulativeLeaveRequests_CumulativeLeaveRequestId",
                table: "Notifications",
                column: "CumulativeLeaveRequestId",
                principalTable: "CumulativeLeaveRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_AdUsers_CreatedById",
                table: "LeaveRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AdUsers_UserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_CumulativeLeaveRequests_CumulativeLeaveRequestId",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaveRequests",
                table: "LeaveRequests");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "Notification");

            migrationBuilder.RenameTable(
                name: "LeaveRequests",
                newName: "LeaveRequest");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserId",
                table: "Notification",
                newName: "IX_Notification_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_CumulativeLeaveRequestId",
                table: "Notification",
                newName: "IX_Notification_CumulativeLeaveRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveRequests_CreatedById",
                table: "LeaveRequest",
                newName: "IX_LeaveRequest_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notification",
                table: "Notification",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaveRequest",
                table: "LeaveRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequest_AdUsers_CreatedById",
                table: "LeaveRequest",
                column: "CreatedById",
                principalTable: "AdUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AdUsers_UserId",
                table: "Notification",
                column: "UserId",
                principalTable: "AdUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_CumulativeLeaveRequests_CumulativeLeaveRequestId",
                table: "Notification",
                column: "CumulativeLeaveRequestId",
                principalTable: "CumulativeLeaveRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
