using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class InitialCreate : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    CategoryName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ParentCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    RoleName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    LoginId = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Portait = table.Column<string>(nullable: true),
                    Gender = table.Column<bool>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ArticleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ContentUrl = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_Articles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "CreatedDate", "Description", "ParentCategoryId", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "新人培训资料库", new DateTime(2019, 2, 15, 19, 56, 15, 910, DateTimeKind.Local).AddTicks(2658), null, 0, null },
                    { 2, "基础营养知识库", new DateTime(2019, 2, 15, 19, 56, 15, 910, DateTimeKind.Local).AddTicks(5304), null, 0, null },
                    { 3, "迷思与常用话术库", new DateTime(2019, 2, 15, 19, 56, 15, 910, DateTimeKind.Local).AddTicks(5329), null, 0, null },
                    { 4, "基础营养知识库-子分类1", new DateTime(2019, 2, 15, 19, 56, 15, 910, DateTimeKind.Local).AddTicks(5330), null, 2, null },
                    { 5, "基础营养知识库-子分类2", new DateTime(2019, 2, 15, 19, 56, 15, 910, DateTimeKind.Local).AddTicks(5331), null, 2, null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedDate", "Description", "RoleName", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 2, 15, 19, 56, 15, 894, DateTimeKind.Local).AddTicks(1677), "管理员", "Admin", null },
                    { 2, new DateTime(2019, 2, 15, 19, 56, 15, 895, DateTimeKind.Local).AddTicks(8909), "公司员工", "Employee", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Active", "CreatedDate", "DateOfBirth", "Email", "FirstName", "Gender", "LastName", "LoginId", "Password", "PhoneNumber", "Portait", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2019, 2, 15, 19, 56, 15, 896, DateTimeKind.Local).AddTicks(627), null, "ycm@outlook.com", "丞民", null, "杨", "ycm@outlook.com", "???F?S???^H[??4", null, null, null },
                    { 2, true, new DateTime(2019, 2, 15, 19, 56, 15, 904, DateTimeKind.Local).AddTicks(9045), null, "12345929@qq.com", "悟空", null, "孙", "12345929@qq.com", "?qc????p0??AR", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "ArticleId", "CategoryId", "ContentUrl", "CreatedDate", "Description", "ImageUrl", "Title", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", new DateTime(2019, 2, 15, 19, 56, 15, 910, DateTimeKind.Local).AddTicks(6000), "article1 description", "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", "article1", null },
                    { 6, 2, "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", new DateTime(2019, 2, 15, 19, 56, 15, 911, DateTimeKind.Local).AddTicks(18), "article6 description", "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", "article6", null },
                    { 7, 3, "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", new DateTime(2019, 2, 15, 19, 56, 15, 911, DateTimeKind.Local).AddTicks(20), "article7 description", "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", "article7", null },
                    { 2, 4, "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", new DateTime(2019, 2, 15, 19, 56, 15, 910, DateTimeKind.Local).AddTicks(9958), "article2 description", "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", "article2", null },
                    { 3, 4, "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", new DateTime(2019, 2, 15, 19, 56, 15, 911, DateTimeKind.Local).AddTicks(13), "article3 description", "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", "article3", null },
                    { 4, 4, "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", new DateTime(2019, 2, 15, 19, 56, 15, 911, DateTimeKind.Local).AddTicks(15), "article4 description", "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", "article4", null },
                    { 5, 5, "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", new DateTime(2019, 2, 15, 19, 56, 15, 911, DateTimeKind.Local).AddTicks(16), "article5 description", "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", "article5", null }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "CreatedDate", "RoleId", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 2, 15, 19, 56, 15, 905, DateTimeKind.Local).AddTicks(801), 1, null, 1 },
                    { 3, new DateTime(2019, 2, 15, 19, 56, 15, 905, DateTimeKind.Local).AddTicks(4793), 2, null, 1 },
                    { 2, new DateTime(2019, 2, 15, 19, 56, 15, 905, DateTimeKind.Local).AddTicks(4759), 2, null, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
