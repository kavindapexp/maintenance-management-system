using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenanceManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddInflationIndexPeriodCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_InflationIndexes_PeriodRange",
                table: "InflationIndexes",
                sql: "\"PeriodEnd\" >= \"PeriodStart\"");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_InflationIndexes_PeriodRange",
                table: "InflationIndexes");
        }
    }
}
