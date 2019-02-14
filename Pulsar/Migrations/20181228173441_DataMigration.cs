using Microsoft.EntityFrameworkCore.Migrations;

namespace Pulsar.Migrations
{
    public partial class DataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    UserID = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServerID = table.Column<ulong>(nullable: false),
                    ItemID = table.Column<ulong>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemID = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServerID = table.Column<ulong>(nullable: false),
                    ItemName = table.Column<string>(nullable: true),
                    ItemDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemID);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    UserID = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServerID = table.Column<ulong>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    ServerID = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EnableModlog = table.Column<bool>(nullable: false),
                    ModLogEmbed = table.Column<bool>(nullable: false),
                    LogBans = table.Column<bool>(nullable: false),
                    LogKicks = table.Column<bool>(nullable: false),
                    LogJoins = table.Column<bool>(nullable: false),
                    LogLeaves = table.Column<bool>(nullable: false),
                    LogUserChanges = table.Column<bool>(nullable: false),
                    LogChannels = table.Column<bool>(nullable: false),
                    LogMessageEdit = table.Column<bool>(nullable: false),
                    LogMessagedelete = table.Column<bool>(nullable: false),
                    LogRoles = table.Column<bool>(nullable: false),
                    LogServer = table.Column<bool>(nullable: false),
                    ModRole = table.Column<ulong>(nullable: false),
                    AdminRole = table.Column<ulong>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.ServerID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Servers");
        }
    }
}
