using FluentMigrator;
using TMA.MovieService.Entities;

namespace TMA.MovieService.Migrations
{
    [Migration(2020_07_03_0_0)]
    public class V2020_07_03_0_0_MovieTableMigration : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("Movie")
                .WithColumn(nameof(MovieEntity.Id)).AsInt32().PrimaryKey().Identity()
                .WithColumn(nameof(MovieEntity.Type)).AsInt32()
                .WithColumn(nameof(MovieEntity.Country)).AsString()
                .WithColumn(nameof(MovieEntity.ExternalId)).AsString()
                .WithColumn(nameof(MovieEntity.ProviderId)).AsString()
                .WithColumn(nameof(MovieEntity.ExternalId2)).AsString()
                .WithColumn(nameof(MovieEntity.TitleEn)).AsString()
                .WithColumn(nameof(MovieEntity.TitleRu)).AsString()
                .WithColumn(nameof(MovieEntity.UpdateTime)).AsDateTime()
                .WithColumn(nameof(MovieEntity.Popularity)).AsDouble()
                .WithColumn(nameof(MovieEntity.Rating)).AsDouble()
                .WithColumn(nameof(MovieEntity.ReleaseYear)).AsInt32()
                ;
        }

        public override void Down()
        {
            Delete.Table("Movie");
        }
    }
}
