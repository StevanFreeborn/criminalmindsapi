using server.Persistence.Seed;
using server.Setup;

if (args.Length > 0 && args[0].ToLower() == "seed")
{
    var seeder = new Seeder();

    if (args.Contains("seasons")) await seeder.SeedSeasonsAsync();

    if (args.Contains("episodes")) await seeder.SeedEpisodesAsync();

    if (args.Contains("quotes")) await seeder.SeedQuotesAsync();

    if (args.Contains("characters")) await seeder.SeedCharactersAsync();
}
else
{
    var app = WebApplication
        .CreateBuilder(args)
        .SetupDb()
        .SetupMvC()
        .SetupSwagger()
        .Build();

    app.SetupMiddleware().Run();
}


