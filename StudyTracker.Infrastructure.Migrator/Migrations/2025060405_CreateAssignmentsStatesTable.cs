using FluentMigrator;

namespace StudyTracker.Infrastructure.Migrator.Migrations;

[Migration(2025060405)]
public class CreateTasksStatesTable : Migration 
{
    public override void Up()
    {
        Execute.Sql("""
                    CREATE TABLE "AssignmentsStates" (
                    "AssignmentId" UUID NOT NULL,
                    "StudentId" UUID NOT NULL,
                    "States" int NOT NULL,
                                                   
                    CONSTRAINT fk_Tasks FOREIGN KEY ("AssignmentId") REFERENCES "Assignments" ("AssignmentId"),
                    CONSTRAINT fk_Students_Task FOREIGN KEY ("StudentId") REFERENCES "Students" ("StudentId"),
                    CONSTRAINT unique_assignment_student UNIQUE ("AssignmentId", "StudentId"));
                    """);
    }

    public override void Down()
    {
        Execute.Sql("""
                    DROP TABLE "AssignmentsStates"
                    """);
    }
}