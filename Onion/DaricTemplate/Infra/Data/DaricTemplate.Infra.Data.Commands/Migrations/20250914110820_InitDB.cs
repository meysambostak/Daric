using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DaricTemplate.Infra.Data.Commands.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountryLocations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationType = table.Column<byte>(type: "tinyint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AlternativeTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ParentCountryLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryLocations", x => x.Id);
                    table.UniqueConstraint("AK_CountryLocations_BusinessId", x => x.BusinessId);
                    table.ForeignKey(
                        name: "FK_CountryLocations_CountryLocations_ParentCountryLocationId",
                        column: x => x.ParentCountryLocationId,
                        principalTable: "CountryLocations",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountryLocations_ParentCountryLocationId",
                table: "CountryLocations",
                column: "ParentCountryLocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryLocations");
        }
    }
}
