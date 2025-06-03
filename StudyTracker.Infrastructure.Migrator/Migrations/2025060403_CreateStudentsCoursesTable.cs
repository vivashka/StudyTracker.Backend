using FluentMigrator;

namespace StudyTracker.Infrastructure.Migrator.Migrations;

[Migration(2025060403)]
public class CreateStudentsCoursesTable : Migration {
    public override void Up()
    {
        Execute.Sql("""
                    CREATE TABLE "StudentsCourses" (
                    "StudentId" UUID NOT NULL,
                    "CourseId" UUID NOT NULL,
                                                   
                    CONSTRAINT fk_Students_Course FOREIGN KEY ("StudentId") REFERENCES "Students" ("StudentId"),
                    CONSTRAINT fk_Courses_Student FOREIGN KEY ("CourseId") REFERENCES "Courses" ("CourseId"));
                    """);
    }

    public override void Down()
    {
        Execute.Sql("""
                    DROP TABLE "StudentsCourses"
                    """);
    }
}