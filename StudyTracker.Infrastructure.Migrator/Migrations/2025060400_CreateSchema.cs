using FluentMigrator;

namespace StudyTracker.Infrastructure.Migrator.Migrations;

[Migration(2025060400)]
public class CreateSchema : Migration
{
    public override void Up()
    {
        Execute.Sql("""
                    CREATE SCHEMA IF NOT EXISTS study_tracker_schema;
                    """);
    }

    public override void Down()
    {
        Execute.Sql("""
                    DROP SCHEMA IF EXISTS study_tracker_schema;
                    """);
    }
}