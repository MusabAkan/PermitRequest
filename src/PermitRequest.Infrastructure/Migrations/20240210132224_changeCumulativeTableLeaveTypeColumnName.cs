using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PermitRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeCumulativeTableLeaveTypeColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LeaveTypeId",
                table: "CumulativeLeaveRequests",
                newName: "LeaveType");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LeaveType",
                table: "CumulativeLeaveRequests",
                newName: "LeaveTypeId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
