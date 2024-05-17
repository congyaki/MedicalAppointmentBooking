using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalAppointmentBooking.WebAPI.Migrations
{
    public partial class UpdateTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Users_UserID",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSpecializations_Doctors_DoctorID",
                table: "DoctorSpecializations");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSpecializations_Specializations_SpecializationID",
                table: "DoctorSpecializations");

            migrationBuilder.RenameColumn(
                name: "SpecializationID",
                table: "DoctorSpecializations",
                newName: "SpecializationId");

            migrationBuilder.RenameColumn(
                name: "DoctorID",
                table: "DoctorSpecializations",
                newName: "DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSpecializations_SpecializationID",
                table: "DoctorSpecializations",
                newName: "IX_DoctorSpecializations_SpecializationId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Doctors",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Doctors_UserID",
                table: "Doctors",
                newName: "IX_Doctors_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Users_UserId",
                table: "Doctors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSpecializations_Doctors_DoctorId",
                table: "DoctorSpecializations",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSpecializations_Specializations_SpecializationId",
                table: "DoctorSpecializations",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Users_UserId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSpecializations_Doctors_DoctorId",
                table: "DoctorSpecializations");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSpecializations_Specializations_SpecializationId",
                table: "DoctorSpecializations");

            migrationBuilder.RenameColumn(
                name: "SpecializationId",
                table: "DoctorSpecializations",
                newName: "SpecializationID");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "DoctorSpecializations",
                newName: "DoctorID");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSpecializations_SpecializationId",
                table: "DoctorSpecializations",
                newName: "IX_DoctorSpecializations_SpecializationID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Doctors",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors",
                newName: "IX_Doctors_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Users_UserID",
                table: "Doctors",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSpecializations_Doctors_DoctorID",
                table: "DoctorSpecializations",
                column: "DoctorID",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSpecializations_Specializations_SpecializationID",
                table: "DoctorSpecializations",
                column: "SpecializationID",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
