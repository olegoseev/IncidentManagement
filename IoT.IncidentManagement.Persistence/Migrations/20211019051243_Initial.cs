using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IoT.IncidentManagement.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionsStore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Interval = table.Column<int>(type: "int", nullable: false),
                    Repeat = table.Column<bool>(type: "bit", nullable: false),
                    InitTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionsStore", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bridges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BridgeType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bridges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationsStore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Group = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Interval = table.Column<int>(type: "int", nullable: false),
                    Repeat = table.Column<bool>(type: "bit", nullable: false),
                    State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationsStore", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Severities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IncidentSeverity = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NotificationInterval = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Severities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IncidentCase = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CustomerImpact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    SeverityId = table.Column<int>(type: "int", nullable: false),
                    BridgeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incidents_Bridges_BridgeId",
                        column: x => x.BridgeId,
                        principalTable: "Bridges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidents_Severities_SeverityId",
                        column: x => x.SeverityId,
                        principalTable: "Severities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidents_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClosureActions",
                columns: table => new
                {
                    IncidentId = table.Column<int>(type: "int", nullable: false),
                    ToDoList = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosureActions", x => x.IncidentId);
                    table.ForeignKey(
                        name: "FK_ClosureActions_Incidents_IncidentId",
                        column: x => x.IncidentId,
                        principalTable: "Incidents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManagerActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IncidentId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Interval = table.Column<int>(type: "int", nullable: false),
                    Repeat = table.Column<bool>(type: "bit", nullable: false),
                    InitTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManagerActions_Incidents_IncidentId",
                        column: x => x.IncidentId,
                        principalTable: "Incidents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Record = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IncidentId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Incidents_IncidentId",
                        column: x => x.IncidentId,
                        principalTable: "Incidents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IncidentId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Group = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Interval = table.Column<int>(type: "int", nullable: false),
                    Repeat = table.Column<bool>(type: "bit", nullable: false),
                    InitTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Incidents_IncidentId",
                        column: x => x.IncidentId,
                        principalTable: "Incidents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    IncidentId = table.Column<int>(type: "int", nullable: false),
                    Group = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.IncidentId);
                    table.ForeignKey(
                        name: "FK_Participants_Incidents_IncidentId",
                        column: x => x.IncidentId,
                        principalTable: "Incidents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ActionsStore",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "InitTime", "Interval", "LastModifiedBy", "LastModifiedDate", "Order", "Repeat", "State" },
                values: new object[,]
                {
                    { 1, null, null, "Action 1", new DateTime(2021, 10, 19, 5, 12, 42, 685, DateTimeKind.Utc).AddTicks(3288), 15, null, null, 1, false, "INITIAL" },
                    { 2, null, null, "Action 2", new DateTime(2021, 10, 19, 5, 12, 42, 685, DateTimeKind.Utc).AddTicks(3532), 15, null, null, 2, false, "INITIAL" },
                    { 3, null, null, "Action 3", new DateTime(2021, 10, 19, 5, 12, 42, 685, DateTimeKind.Utc).AddTicks(3534), 15, null, null, 3, true, "INITIAL" },
                    { 4, null, null, "Action 4", new DateTime(2021, 10, 19, 5, 12, 42, 685, DateTimeKind.Utc).AddTicks(3535), 15, null, null, 4, true, "INITIAL" },
                    { 5, null, null, "Action 5", new DateTime(2021, 10, 19, 5, 12, 42, 685, DateTimeKind.Utc).AddTicks(3536), 15, null, null, 5, true, "INITIAL" },
                    { 6, null, null, "Action 6", new DateTime(2021, 10, 19, 5, 12, 42, 685, DateTimeKind.Utc).AddTicks(3537), 15, null, null, 6, false, "INITIAL" }
                });

            migrationBuilder.InsertData(
                table: "Bridges",
                columns: new[] { "Id", "BridgeType", "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate" },
                values: new object[,]
                {
                    { 1, "IoT TS", "Initial Migration", new DateTime(2021, 10, 19, 5, 12, 42, 684, DateTimeKind.Utc).AddTicks(1242), "Initial Migration", new DateTime(2021, 10, 19, 5, 12, 42, 684, DateTimeKind.Utc).AddTicks(1842) },
                    { 2, "CMD", "Initial Migration", new DateTime(2021, 10, 19, 5, 12, 42, 684, DateTimeKind.Utc).AddTicks(2538), "Initial Migration", new DateTime(2021, 10, 19, 5, 12, 42, 684, DateTimeKind.Utc).AddTicks(2540) },
                    { 3, "NOC", "Initial Migration", new DateTime(2021, 10, 19, 5, 12, 42, 684, DateTimeKind.Utc).AddTicks(2542), "Initial Migration", new DateTime(2021, 10, 19, 5, 12, 42, 684, DateTimeKind.Utc).AddTicks(2543) }
                });

            migrationBuilder.InsertData(
                table: "NotificationsStore",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Group", "Interval", "LastModifiedBy", "LastModifiedDate", "Order", "Repeat", "State", "Type" },
                values: new object[,]
                {
                    { 6, null, null, "EXTERNAL", 15, null, null, 3, false, "INITIAL", "FINAL" },
                    { 5, null, null, "EXTERNAL", 15, null, null, 2, true, "INITIAL", "UPDATE" },
                    { 4, null, null, "EXTERNAL", 15, null, null, 1, false, "INITIAL", "INITIAL" },
                    { 2, null, null, "INTERNAL", 15, null, null, 2, true, "INITIAL", "UPDATE" },
                    { 1, null, null, "INTERNAL", 15, null, null, 1, false, "INITIAL", "INITIAL" },
                    { 3, null, null, "INTERNAL", 15, null, null, 3, false, "INITIAL", "FINAL" }
                });

            migrationBuilder.InsertData(
                table: "Severities",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IncidentSeverity", "LastModifiedBy", "LastModifiedDate", "NotificationInterval" },
                values: new object[,]
                {
                    { 1, "Initial Migration", new DateTime(2021, 10, 19, 5, 12, 42, 685, DateTimeKind.Utc).AddTicks(1623), "P1", "Initial Migration", new DateTime(2021, 10, 19, 5, 12, 42, 685, DateTimeKind.Utc).AddTicks(1625), 60 },
                    { 2, "Initial Migration", new DateTime(2021, 10, 19, 5, 12, 42, 685, DateTimeKind.Utc).AddTicks(1628), "P2", "Initial Migration", new DateTime(2021, 10, 19, 5, 12, 42, 685, DateTimeKind.Utc).AddTicks(1629), 90 },
                    { 3, "Initial Migration", new DateTime(2021, 10, 19, 5, 12, 42, 685, DateTimeKind.Utc).AddTicks(1630), "P3", "Initial Migration", new DateTime(2021, 10, 19, 5, 12, 42, 685, DateTimeKind.Utc).AddTicks(1631), 120 }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "CurrentStatus", "LastModifiedBy", "LastModifiedDate" },
                values: new object[,]
                {
                    { 2, "Initial Migration", new DateTime(2021, 10, 19, 5, 12, 42, 685, DateTimeKind.Utc).AddTicks(590), "Inactive", "Initial Migration", new DateTime(2021, 10, 19, 5, 12, 42, 685, DateTimeKind.Utc).AddTicks(591) },
                    { 1, "Initial Migration", new DateTime(2021, 10, 19, 5, 12, 42, 685, DateTimeKind.Utc).AddTicks(583), "Active", "Initial Migration", new DateTime(2021, 10, 19, 5, 12, 42, 685, DateTimeKind.Utc).AddTicks(587) },
                    { 3, "Initial Migration", new DateTime(2021, 10, 19, 5, 12, 42, 685, DateTimeKind.Utc).AddTicks(593), "Postponed", "Initial Migration", new DateTime(2021, 10, 19, 5, 12, 42, 685, DateTimeKind.Utc).AddTicks(593) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClosureActions_IncidentId",
                table: "ClosureActions",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_BridgeId",
                table: "Incidents",
                column: "BridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_IncidentCase",
                table: "Incidents",
                column: "IncidentCase");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_SeverityId",
                table: "Incidents",
                column: "SeverityId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_StatusId",
                table: "Incidents",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerActions_IncidentId",
                table: "ManagerActions",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_IncidentId",
                table: "Notes",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_IncidentId",
                table: "Notifications",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_IncidentId",
                table: "Participants",
                column: "IncidentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionsStore");

            migrationBuilder.DropTable(
                name: "ClosureActions");

            migrationBuilder.DropTable(
                name: "ManagerActions");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "NotificationsStore");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Incidents");

            migrationBuilder.DropTable(
                name: "Bridges");

            migrationBuilder.DropTable(
                name: "Severities");

            migrationBuilder.DropTable(
                name: "Statuses");
        }
    }
}
