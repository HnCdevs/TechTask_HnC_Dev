using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalTask.Migrations
{
    public partial class ChangeTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Business_Country_CountryId",
                table: "Business");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_Offering_OfferingId",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Family_Business_BusinessId",
                table: "Family");

            migrationBuilder.DropForeignKey(
                name: "FK_Offering_Family_FamilyId",
                table: "Offering");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationCountries_Country_CountryId",
                table: "OrganizationCountries");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationCountries_Organization_OrganizationId",
                table: "OrganizationCountries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organization",
                table: "Organization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offering",
                table: "Offering");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Family",
                table: "Family");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Department",
                table: "Department");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Business",
                table: "Business");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Organization",
                newName: "Organizations");

            migrationBuilder.RenameTable(
                name: "Offering",
                newName: "Offerings");

            migrationBuilder.RenameTable(
                name: "Family",
                newName: "Families");

            migrationBuilder.RenameTable(
                name: "Department",
                newName: "Departments");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Countries");

            migrationBuilder.RenameTable(
                name: "Business",
                newName: "Businesses");

            migrationBuilder.RenameIndex(
                name: "IX_Offering_FamilyId",
                table: "Offerings",
                newName: "IX_Offerings_FamilyId");

            migrationBuilder.RenameIndex(
                name: "IX_Family_BusinessId",
                table: "Families",
                newName: "IX_Families_BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_Department_OfferingId",
                table: "Departments",
                newName: "IX_Departments_OfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Business_CountryId",
                table: "Businesses",
                newName: "IX_Businesses_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offerings",
                table: "Offerings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Families",
                table: "Families",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Businesses",
                table: "Businesses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_Countries_CountryId",
                table: "Businesses",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Offerings_OfferingId",
                table: "Departments",
                column: "OfferingId",
                principalTable: "Offerings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Families_Businesses_BusinessId",
                table: "Families",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offerings_Families_FamilyId",
                table: "Offerings",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationCountries_Countries_CountryId",
                table: "OrganizationCountries",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationCountries_Organizations_OrganizationId",
                table: "OrganizationCountries",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_Countries_CountryId",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Offerings_OfferingId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Families_Businesses_BusinessId",
                table: "Families");

            migrationBuilder.DropForeignKey(
                name: "FK_Offerings_Families_FamilyId",
                table: "Offerings");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationCountries_Countries_CountryId",
                table: "OrganizationCountries");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationCountries_Organizations_OrganizationId",
                table: "OrganizationCountries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offerings",
                table: "Offerings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Families",
                table: "Families");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Businesses",
                table: "Businesses");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Organizations",
                newName: "Organization");

            migrationBuilder.RenameTable(
                name: "Offerings",
                newName: "Offering");

            migrationBuilder.RenameTable(
                name: "Families",
                newName: "Family");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "Department");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Country");

            migrationBuilder.RenameTable(
                name: "Businesses",
                newName: "Business");

            migrationBuilder.RenameIndex(
                name: "IX_Offerings_FamilyId",
                table: "Offering",
                newName: "IX_Offering_FamilyId");

            migrationBuilder.RenameIndex(
                name: "IX_Families_BusinessId",
                table: "Family",
                newName: "IX_Family_BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_OfferingId",
                table: "Department",
                newName: "IX_Department_OfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Businesses_CountryId",
                table: "Business",
                newName: "IX_Business_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organization",
                table: "Organization",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offering",
                table: "Offering",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Family",
                table: "Family",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Department",
                table: "Department",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Business",
                table: "Business",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Business_Country_CountryId",
                table: "Business",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Offering_OfferingId",
                table: "Department",
                column: "OfferingId",
                principalTable: "Offering",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Family_Business_BusinessId",
                table: "Family",
                column: "BusinessId",
                principalTable: "Business",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offering_Family_FamilyId",
                table: "Offering",
                column: "FamilyId",
                principalTable: "Family",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationCountries_Country_CountryId",
                table: "OrganizationCountries",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationCountries_Organization_OrganizationId",
                table: "OrganizationCountries",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
