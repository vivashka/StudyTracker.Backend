using FluentMigrator;

namespace StudyTracker.Infrastructure.Migrator.Migrations;

[Migration(2025060401)]
public class CreateStudentsTable : Migration
{
    public override void Up()
    {
        Execute.Sql("""
                     CREATE TABLE "Students" ("StudentId" UUID PRIMARY KEY,
                     "Login" text not null,
                     "Password" text not null);
                     """);
    }

    public override void Down()
    {
        Execute.Sql("""
                    DROP TABLE "Students"
                    """);
    }
}