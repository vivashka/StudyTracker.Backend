using FluentMigrator;

namespace StudyTracker.Infrastructure.Migrator.Migrations;

[Migration(2025060404)]
public class CreateAssignmentsTable : Migration {
    public override void Up()
    {
        Execute.Sql("""
                    CREATE TABLE "Assignments" ("AssignmentId" UUID PRIMARY KEY,
                    "Name" text UNIQUE,
                    "Description" text,
                    "CourseId" uuid NOT NULL,
                    "Deadline" timestamp,
                    CONSTRAINT fk_Courses_Task FOREIGN KEY ("CourseId") REFERENCES "Courses" ("CourseId") ON DELETE CASCADE);
                    """);
    }

    public override void Down()
    {
        Execute.Sql("""
                    DROP TABLE "Assignments"
                    """);
    }
}