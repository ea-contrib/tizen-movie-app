using FluentMigrator;
using TMA.PrincipalService.Entities;

namespace TMA.PrincipalService.Migrations
{
    [Migration(2020_07_02_1_0)]
    public class V2020_07_02_1_0_GrantTableMigration : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("Grant")
                .WithColumn(nameof(GrantEntity.Id)).AsInt32().PrimaryKey().Identity()
                .WithColumn(nameof(GrantEntity.Key)).AsString()
                .WithColumn(nameof(GrantEntity.Type)).AsString()
                .WithColumn(nameof(GrantEntity.SubjectId)).AsString()
                .WithColumn(nameof(GrantEntity.ClientId)).AsString()
                .WithColumn(nameof(GrantEntity.CreationTime)).AsDateTime()
                .WithColumn(nameof(GrantEntity.Expiration)).AsDateTime()
                .WithColumn(nameof(GrantEntity.Data)).AsString()
                ;
        }

        public override void Down()
        {
            Delete.Table("Grant");
        }
    }
}