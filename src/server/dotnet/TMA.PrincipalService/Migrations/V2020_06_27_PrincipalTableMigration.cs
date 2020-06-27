using FluentMigrator;
using TMA.PrincipalService.Entities;

namespace TMA.PrincipalService.Migrations
{
    [Migration(2020_06_27_0_0)]
    public class V2020_06_27_0_0_PrincipalTableMigration : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("Principal")
                .WithColumn(nameof(PrincipalEntity.Id)).AsInt32().PrimaryKey().Identity()
                .WithColumn(nameof(PrincipalEntity.Name)).AsString()
                .WithColumn(nameof(PrincipalEntity.Email)).AsString()
                .WithColumn(nameof(PrincipalEntity.PasswordHash)).AsString()
                .WithColumn(nameof(PrincipalEntity.PasswordSalt)).AsString()
                ;
        }

        public override void Down()
        {
            Delete.Table("Principal");
        }
    }
}
