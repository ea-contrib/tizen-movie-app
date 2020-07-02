using FluentMigrator;
using TMA.PrincipalService.Entities;

namespace TMA.PrincipalService.Migrations
{
    [Migration(2020_07_02_0_0)]
    public class V2020_07_02_0_0_DefaultPrincipalMigration : FluentMigrator.Migration
    {
        public override void Up()
        {
            Insert.IntoTable("Principal")
                .Row(new PrincipalEntity
                {
                    Name = "Default user",
                    Email = "user@user.com",
                    PasswordHash = "4mCkyXT5nPvaM2rkA0+j1xW04InsI2EDbW2cdN8E5BZxF+4p48SLgrUOkwS6vtB04Zo8qH8aXrUhD7eZZoZdxA==",
                    PasswordSalt = "Jrx/pAk0TT/37ChfoozhFHOmaUU7Rnr2OULGzg0KIFoVB3PNBF2J7cFnEOp64goNHbL6JoWVXxAW7A+hq09Ayw=="
                });
        }

        public override void Down()
        {
        }
    }
}