using GameStore.Api.Dtos;
using System;
using System.Collections.Generic;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static List<GameDto> games = new List<GameDto>
    {
        new(1,
            "The Legend of Zelda: Tears of the Kingdom",
            "Adventure",
            69.99m,
            new DateOnly(2023, 5, 12)),
        new(2,
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
    };

    public static WebApplication MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        // GET /games
        group.MapGet("/", () => games);

        // GET /games/{id}
        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(g => g.Id == id);
            return game is not null ? Results.Ok(game) : Results.NotFound();
        }).WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            var game = new GameDto(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        // PUT /games/{id}
        group.MapPut("/{id}", (int id, UpdateGameDto updateGameDto) =>
        {
            var index = games.FindIndex(g => g.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updateGameDto.Name,
                updateGameDto.Genre,
                updateGameDto.Price,
                updateGameDto.ReleaseDate
            );

            return Results.NoContent();
        });

        // DELETE /games/{id}
        group.MapDelete("/{id}", (int id) =>
        {
            var game = games.Find(g => g.Id == id);
            if (game is null)
            {
                return Results.NotFound();
            }

            games.Remove(game);
            return Results.NoContent();
        });

        return app;
    }
}
