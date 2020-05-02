using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreDb.Migrations
{
    public partial class UserLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Customers ADD COLUMN Login TEXT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
