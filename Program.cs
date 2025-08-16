using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games =
[
    new(1,
        "The Legend of Zelda: Tears of the Kingdom",
        "Adventure",
        69.99m,
        new DateOnly(2023, 5, 12)),
    new(
        2,
        "Elden Ring",
        "Action RPG",
        59.99m,
        new DateOnly(2022, 2, 25)),
    new(3,
        "Hades II",
        "Roguelike",
        29.99m,
        new DateOnly(2024, 6, 15)),
    new(4,
        "Minecraft",
        "Sandbox",
        26.95m,
        new DateOnly(2011, 11, 18)),
    new(5,
        "Stardew Valley",
        "Simulation",
        14.99m,
        new DateOnly(2016, 2, 26))
];

// GET /games
app.MapGet("games", () => games);

// GET /games/{id}
app.MapGet("games/{id}", (int id) =>
{
    var game = games.Find(g => g.Id == id);
    return game is not null ? Results.Ok(game) : Results.NotFound();
}).WithName(GetGameEndpointName);

// POST /games
app.MapPost("games", (CreateGameDto newGame) =>
{
    var game = new GameDto(
        games.Count + 1,
        newGame.Name,
        newGame.Gender,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});

// PUT /games/{id}
app.MapPut("games/{id}", (int id, UpdateGameDto updateGameDto) =>
{
    var index = games.FindIndex(g => g.Id == id);
    if (index == -1)
    {
        return Results.NotFound();
    }

    games[index] = new GameDto(
        id,
        updateGameDto.Name,
        updateGameDto.Gender,
        updateGameDto.Price,
        updateGameDto.ReleaseDate
    );

    return Results.NoContent();
});

// DELETE /games/{id}
app.MapDelete("games/{id}", (int id) =>
{
    var game = games.Find(g => g.Id == id);
    if (game is null)
    {
        return Results.NotFound();
    }

    games.Remove(game);
    return Results.NoContent();
});

app.Run();
