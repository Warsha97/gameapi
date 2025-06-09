using MyTestDotnetApp.Dtos;

var builder = WebApplication.CreateBuilder(args);
//This is where we configure this web application with all
//services, middlewear etc.

var app = builder.Build();


const string getGameEndpoint = "GetGame";

List < GameDto > games = [
    new GameDto(1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
    new GameDto(2, "Final Fantasy XIV", "Roleplaying", 59.99M, new DateOnly(2010, 9, 30)),
    new GameDto(3, "FIFA 23", "Sports", 69.99M, new DateOnly(2022, 9, 27))];

// GET /games
app.MapGet("games", () => games);

// GET /games/{id}
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id)).WithName(getGameEndpoint);

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
    return Results.CreatedAtRoute(getGameEndpoint, new { Id = game.Id}, game);
});



// PUT /games/{id}
app.MapPut("games/{id}", (int id, UpdateGameDto updateDto) =>
{
    int index = games.FindIndex(game => game.Id == id);
    games[index] = new GameDto(id, updateDto.Name, updateDto.Genre, updateDto.Price, updateDto.ReleaseDate);
    return Results.NoContent();

});


// DELETE /games/{id}
app.MapDelete("games/{id}", (int id) =>
{
    games.RemoveAll(game => game.Id == id);
    return Results.NoContent();
});

app.Run();
