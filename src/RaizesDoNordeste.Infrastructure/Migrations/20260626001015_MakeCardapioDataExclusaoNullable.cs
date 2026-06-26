using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaizesDoNordeste.Infrastructure.Migrations;

/// <inheritdoc />
public partial class MakeCardapioDataExclusaoNullable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "IE",
            table: "Unidades",
            newName: "Ie");

        migrationBuilder.RenameColumn(
            name: "CNPJ",
            table: "Unidades",
            newName: "Cnpj");

        migrationBuilder.AlterColumn<DateTime>(
            name: "DataExclusao",
            table: "Cardapios",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Ie",
            table: "Unidades",
            newName: "IE");

        migrationBuilder.RenameColumn(
            name: "Cnpj",
            table: "Unidades",
            newName: "CNPJ");

        migrationBuilder.AlterColumn<DateTime>(
            name: "DataExclusao",
            table: "Cardapios",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);
    }
}
