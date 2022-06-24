using server.Persistence.Seed;
using server.Setup;

if (args.Length == 2 && args[0].ToLower() == "seed")
{
    var seeder = new Seeder();

    if (args[1].ToLower() == "seasons")
    {
        await seeder.SeedSeasonsAsync();
    }

    if (args[1].ToLower() == "episodes")
    {
        await seeder.SeedEpisodesAsync();
    }

    if (args[1].ToLower() == "quotes")
    {
        await seeder.SeedQuotesAsync();
    }

    if (args[1].ToLower() == "characters")
    {
        await seeder.SeedCharactersAsync();
    }
}
else
{
    var builder = WebApplication.CreateBuilder(args);

    Services.SetupDb(builder);
    Services.SetupMvC(builder);
    Services.SetupSwagger(builder);

    var app = builder.Build();

    Middleware.Setup(app);

    app.Run();
}


