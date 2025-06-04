using FluentMigrator;

namespace StudyTracker.Infrastructure.Migrator.Migrations;

[Migration(2025060402)]
public class CreateCoursesTable : Migration
{
    public override void Up()
    {
        Execute.Sql("""
                    CREATE TABLE "Courses" ("CourseId" UUID PRIMARY KEY,
                    "Name" text not null UNIQUE,
                    "Description" text,
                    "Professor" text not null);
                    """);
    }

    public override void Down()
    {
        Execute.Sql("""
                    DROP TABLE "Courses"
                    """);
    }
}