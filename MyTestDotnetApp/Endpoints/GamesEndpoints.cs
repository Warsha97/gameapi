using MyTestDotnetApp.Dtos;

namespace MyTestDotnetApp.Endpoints
{
    public static class GamesEndpoints
    {
        const string getGameEndpoint = "GetGame";

        private static List<GameDto> games = [
            new (1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
            new (2, "Final Fantasy XIV", "Roleplaying", 59.99M, new DateOnly(2010, 9, 30)),
            new (3, "FIFA 23", "Sports", 69.99M, new DateOnly(2022, 9, 27))
            ];

        public static WebApplication MapGamesEndpoint(this WebApplication app)
        {
            // GET /games
            app.MapGet("games", () => games);

            // GET /games/{id}
            app.MapGet("games/{id}", (int id) =>
            {
                GameDto? game = games.Find(game => game.Id == id);
                return game is null ? Results.NotFound() : Results.Ok(game);
            })
            .WithName(getGameEndpoint);


            // POST /games
            app.MapPost("games", (CreateGameDto newGame) =>
            {
                GameDto game = new GameDto(
                    games.Count + 1,
                    newGame.Name,
                    newGame.Genre,
                    newGame.Price,
                    newGame.ReleaseDate);
                games.Add(game);
                return Results.CreatedAtRoute(getGameEndpoint, new { Id = game.Id }, game);
            });



            // PUT /games/{id}
            app.MapPut("games/{id}", (int id, UpdateGameDto updateDto) =>
            {
                int index = games.FindIndex(game => game.Id == id);

                if (index == -1)
                    return Results.NotFound();

                games[index] = new GameDto(id, updateDto.Name, updateDto.Genre, updateDto.Price, updateDto.ReleaseDate);
                return Results.NoContent();

            });


            // DELETE /games/{id}
            app.MapDelete("games/{id}", (int id) =>
            {
                games.RemoveAll(game => game.Id == id);
                return Results.NoContent();
            });

            return app;
        }


    }
}

   
