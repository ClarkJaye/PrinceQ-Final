using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrinceQ.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Announcement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2(7)", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    DeviceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClerkNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.DeviceId);
                });

            migrationBuilder.CreateTable(
                name: "IsActive",
                columns: table => new
                {
                    IsActiveId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IsActive", x => x.IsActiveId);
                });

            migrationBuilder.CreateTable(
                name: "Queue_Statuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queue_Statuses", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Stage",
                columns: table => new
                {
                    StageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stage", x => x.StageId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActiveId = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_IsActive_IsActiveId",
                        column: x => x.IsActiveId,
                        principalTable: "IsActive",
                        principalColumn: "IsActiveId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActiveId = table.Column<int>(type: "int", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_IsActive_IsActiveId",
                        column: x => x.IsActiveId,
                        principalTable: "IsActive",
                        principalColumn: "IsActiveId");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clerk_Serve_ForFilling",
                columns: table => new
                {
                    GenerateDate = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClerkId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    QueueNumber = table.Column<int>(type: "int", nullable: false),
                    Serve_start = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Serve_end = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clerk_Serve_ForFilling", x => new { x.GenerateDate, x.ClerkId, x.CategoryId, x.QueueNumber });
                    table.ForeignKey(
                        name: "FK_Clerk_Serve_ForFilling_AspNetUsers_ClerkId",
                        column: x => x.ClerkId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clerk_Serve_ForFilling_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clerk_Serve_Releasing",
                columns: table => new
                {
                    GenerateDate = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClerkId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    QueueNumber = table.Column<int>(type: "int", nullable: false),
                    Serve_start = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Serve_end = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clerk_Serve_Releasing", x => new { x.GenerateDate, x.ClerkId, x.CategoryId, x.QueueNumber });
                    table.ForeignKey(
                        name: "FK_Clerk_Serve_Releasing_AspNetUsers_ClerkId",
                        column: x => x.ClerkId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clerk_Serve_Releasing_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QueueNumbers",
                columns: table => new
                {
                    QueueId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    QueueNumber = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    ForFilling_start = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ForFilling_end = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Releasing_start = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Releasing_end = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Reserve_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cancelled_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClerkId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueueNumbers", x => new { x.QueueId, x.CategoryId, x.QueueNumber });
                    table.ForeignKey(
                        name: "FK_QueueNumbers_AspNetUsers_ClerkId",
                        column: x => x.ClerkId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QueueNumbers_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QueueNumbers_Queue_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Queue_Statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QueueNumbers_Stage_StageId",
                        column: x => x.StageId,
                        principalTable: "Stage",
                        principalColumn: "StageId");
                });

            migrationBuilder.CreateTable(
                name: "Serving",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    QueueNumberServe = table.Column<int>(type: "int", nullable: false),
                    Served_At = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serving", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Serving_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Serving_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Category",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Category", x => new { x.UserId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_User_Category_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Category_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "18ab63db-22b1-4656-93e8-6240c08c988c", null, "Admin", "ADMIN" },
                    { "5671e11c-00b4-4302-9214-c3b7f7b71188", null, "RegisterPersonnel", "REGISTERPERSONNEL" },
                    { "fbc43974-ddf4-4fed-8a0b-42e6897f259f", null, "Clerk", "CLERK" }
                });

            migrationBuilder.InsertData(
                table: "Device",
                columns: new[] { "DeviceId", "ClerkNumber" },
                values: new object[] { "00330-71344-74698-AAOEM", "Clerk 1" });

            migrationBuilder.InsertData(
                table: "IsActive",
                columns: new[] { "IsActiveId", "Name" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Inactive" }
                });

            migrationBuilder.InsertData(
                table: "Queue_Statuses",
                columns: new[] { "StatusId", "StatusName" },
                values: new object[,]
                {
                    { 1, "waiting" },
                    { 2, "serve" },
                    { 3, "reserve" },
                    { 4, "cancel" },
                    { 5, "serving" }
                });

            migrationBuilder.InsertData(
                table: "Stage",
                columns: new[] { "StageId", "StageName" },
                values: new object[,]
                {
                    { 1, "Filling" },
                    { 2, "Releasing" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsActiveId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3386761a-6384-4e97-9eb3-d2d09e6bfec5", 0, "79681f10-0ee2-4127-9e9c-0eb73ded94fe", "user1@princeretail.com", false, 1, true, null, "USER1@PRINCERETAIL.COM", "USER1", "AQAAAAIAAYagAAAAECnnqwWvQxblMxWGHvFRkui6EcfZu6BPqf2MtI8fZS9u6gCf8BWu3bZIc1xF16W6zA==", null, false, "GJ636UXBBLKO5JOGP6X3WISMIEBRVVHM", false, "user1" },
                    { "5817a627-dcb6-4612-85e5-13b56dc52560", 0, "c935029b-a917-4967-b007-18bd2ccaa9fb", "user2@princeretail.com", false, 1, true, null, "USER2@PRINCERETAIL.COM", "USER2", "AQAAAAIAAYagAAAAEHVWq2Lhee6lgjGXwYSWbtfzS4JYT0MEYWaOs6USpgbOBGJEaO1KB8YxDLviUH8eag==", null, false, "TH3JDB7PM7WMDGG4OYPLQKLGA5ECC4T3", false, "user2" },
                    { "6a3e3e42-a9d7-4ce4-b97a-1f3a3007c8b4", 0, "ecfd0212-7008-485f-aca1-ba1c17ac009c", "user3@princeretail.com", false, 1, true, null, "USER3@PRINCERETAIL.COM", "USER3", "AQAAAAIAAYagAAAAEOIf7eXfV9kI/xsnNgDanLoBQLRGl1+jrl1+BJvGqblI7v/VTSx4C3/LJTGi4oBC7Q==", null, false, "5BJYPNN2TLNSI55R6RGCHCK4F3Z4QCXA", false, "user3" },
                    { "cadf9177-f64e-4ab6-bb37-f50770ef67b5", 0, "8b25f225-70d8-48be-a06f-46a91475d5f8", "personnel1@princeretail.com", false, 1, true, null, "PERSONNEL1@PRINCERETAIL.COM", "PERSONNEL1", "AQAAAAIAAYagAAAAEHX+4UDDHRNT58qxXn8u5YlDjkKG+bJTVvoDKEvtOPFF3/z+o/4ZbJ3CIv0+W/mZjw==", null, false, "6WJLAWNDIARO6JH6N3T7AM224IYQLRO4", false, "personnel1" },
                    { "f626b751-35a0-43df-8173-76cb5b4886fd", 0, "68094bf9-de7e-4b21-a651-b741ae57c6aa", "admin@princeretail.com", false, 1, true, null, "ADMIN@PRINCERETAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEJpB1HfJeqVp4SjIDJMOHVIEmfY55M/N7YsXHENkNlge5P6k15rYKenBWVNTobCO0Q==", null, false, "N36KZ2J262FG2K5NWBB3WESIUXPLZ5WH", false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description", "IsActiveId" },
                values: new object[,]
                {
                    { 1, "Category A", "Trade", 1 },
                    { 2, "Category B", "Non-Trade", 1 },
                    { 3, "Category C", "Special", 1 },
                    { 4, "Category D", "Inquiry", 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "fbc43974-ddf4-4fed-8a0b-42e6897f259f", "3386761a-6384-4e97-9eb3-d2d09e6bfec5" },
                    { "fbc43974-ddf4-4fed-8a0b-42e6897f259f", "5817a627-dcb6-4612-85e5-13b56dc52560" },
                    { "fbc43974-ddf4-4fed-8a0b-42e6897f259f", "6a3e3e42-a9d7-4ce4-b97a-1f3a3007c8b4" },
                    { "5671e11c-00b4-4302-9214-c3b7f7b71188", "cadf9177-f64e-4ab6-bb37-f50770ef67b5" },
                    { "18ab63db-22b1-4656-93e8-6240c08c988c", "f626b751-35a0-43df-8173-76cb5b4886fd" }
                });

            migrationBuilder.InsertData(
                table: "User_Category",
                columns: new[] { "CategoryId", "UserId" },
                values: new object[,]
                {
                    { 1, "3386761a-6384-4e97-9eb3-d2d09e6bfec5" },
                    { 2, "3386761a-6384-4e97-9eb3-d2d09e6bfec5" },
                    { 3, "3386761a-6384-4e97-9eb3-d2d09e6bfec5" },
                    { 4, "3386761a-6384-4e97-9eb3-d2d09e6bfec5" },
                    { 1, "5817a627-dcb6-4612-85e5-13b56dc52560" },
                    { 2, "5817a627-dcb6-4612-85e5-13b56dc52560" },
                    { 3, "5817a627-dcb6-4612-85e5-13b56dc52560" },
                    { 4, "5817a627-dcb6-4612-85e5-13b56dc52560" },
                    { 1, "6a3e3e42-a9d7-4ce4-b97a-1f3a3007c8b4" },
                    { 2, "6a3e3e42-a9d7-4ce4-b97a-1f3a3007c8b4" },
                    { 3, "6a3e3e42-a9d7-4ce4-b97a-1f3a3007c8b4" },
                    { 4, "6a3e3e42-a9d7-4ce4-b97a-1f3a3007c8b4" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IsActiveId",
                table: "AspNetUsers",
                column: "IsActiveId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IsActiveId",
                table: "Categories",
                column: "IsActiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Clerk_Serve_ForFilling_CategoryId",
                table: "Clerk_Serve_ForFilling",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Clerk_Serve_ForFilling_ClerkId",
                table: "Clerk_Serve_ForFilling",
                column: "ClerkId");

            migrationBuilder.CreateIndex(
                name: "IX_Clerk_Serve_Releasing_CategoryId",
                table: "Clerk_Serve_Releasing",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Clerk_Serve_Releasing_ClerkId",
                table: "Clerk_Serve_Releasing",
                column: "ClerkId");

            migrationBuilder.CreateIndex(
                name: "IX_QueueNumbers_CategoryId",
                table: "QueueNumbers",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_QueueNumbers_ClerkId",
                table: "QueueNumbers",
                column: "ClerkId");

            migrationBuilder.CreateIndex(
                name: "IX_QueueNumbers_StageId",
                table: "QueueNumbers",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_QueueNumbers_StatusId",
                table: "QueueNumbers",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Serving_CategoryId",
                table: "Serving",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Serving_UserId",
                table: "Serving",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Category_CategoryId",
                table: "User_Category",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcement");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Clerk_Serve_ForFilling");

            migrationBuilder.DropTable(
                name: "Clerk_Serve_Releasing");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "QueueNumbers");

            migrationBuilder.DropTable(
                name: "Serving");

            migrationBuilder.DropTable(
                name: "User_Category");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Queue_Statuses");

            migrationBuilder.DropTable(
                name: "Stage");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "IsActive");
        }
    }
}
