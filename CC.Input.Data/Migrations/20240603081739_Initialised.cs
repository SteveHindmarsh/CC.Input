using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CC.Input.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initialised : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inputs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MPAN = table.Column<long>(type: "bigint", nullable: false),
                    MeterSerial = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DateOfInstallation = table.Column<DateOnly>(type: "date", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inputs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inputs");
        }
    }
}
