using MyTestDotnetApp.Data;
using MyTestDotnetApp.Dtos;
using MyTestDotnetApp.Entities;

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

        public static RouteGroupBuilder MapGamesEndpoint(this WebApplication app)
        {
            var group = app.MapGroup("games")
                            .WithParameterValidation();
            // GET /games
            group.MapGet("/", () => games);

            // GET /games/{id}
            group.MapGet("/{id}", (int id) =>
            {
                GameDto? game = games.Find(game => game.Id == id);
                return game is null ? Results.NotFound() : Results.Ok(game);
            })
            .WithName(getGameEndpoint);


            // POST /games
            /*Dependency injection of dbcontext*/
            group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
            {
                Game game = new Game()
                {
                    Name = newGame.Name,
                    Genre = dbContext.Genres.Find(newGame.GenreId),
                    Price = newGame.Price,
                    ReleaseDate = newGame.ReleaseDate
                };
               
                dbContext.Games.Add(game); // adding this to db context and letting entity framework know
                dbContext.SaveChanges(); // Actual conversion from entity framework to SQL

                GameDto gameDto = new(game.Id, game.Name, game.Genre.Name, game.Price, game.ReleaseDate);


                return Results.CreatedAtRoute(getGameEndpoint, new { Id = game.Id }, gameDto);
            });



            // PUT /games/{id}
            group.MapPut("/{id}", (int id, UpdateGameDto updateDto) =>
            {
                int index = games.FindIndex(game => game.Id == id);

                if (index == -1)
                    return Results.NotFound();

                games[index] = new GameDto(id, updateDto.Name, updateDto.Genre, updateDto.Price, updateDto.ReleaseDate);
                return Results.NoContent();

            });


            // DELETE /games/{id}
            group.MapDelete("/{id}", (int id) =>
            {
                games.RemoveAll(game => game.Id == id);
                return Results.NoContent();
            });

            return group;
        }


    }
}

   
