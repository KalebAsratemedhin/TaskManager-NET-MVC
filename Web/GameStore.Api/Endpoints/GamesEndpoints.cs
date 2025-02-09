using System;
using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    private static List<GameDto> games = [
        new (1, "Street Fighter II", "Fighting",19.9M, new DateOnly(1992, 4, 5) ),
        new (2, "Final Fantasy XIV", "RolePlaying",9.9M, new DateOnly(1982, 10, 5) ),
        new (3, "FIFA 23", "Sports",69.9M, new DateOnly(2022, 1, 29) ),
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        group.MapGet("/", () => games);

        group.MapGet("/{id}", (int id) => {
            GameDto? game = games.Find((game) => game.Id == id);

            if(game is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(game);
        }).WithName("GetGame");

        group.MapPost("/", (CreateGameDto newGame) => {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
            games.Add(game);

            return Results.CreatedAtRoute("GetGame", new {id = game.Id}, game);
        });

        group.MapPut("/{id}", (int id, UpdateGameDto newGame) => {
            var index = games.FindIndex((game) => game.Id == id);

            if(index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new(
                id,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );


            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) => {
            games.RemoveAll((game) => game.Id == id);
            return Results.NoContent();
        });

        return group;

    }

}
