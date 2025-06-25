using System.ComponentModel.DataAnnotations;

namespace MyTestDotnetApp.Dtos
{
    public record class CreateGameDto(
        [Required][StringLength(50)]string Name,
        int GenreId,
        [Range(1,1000)]decimal Price,
        DateOnly ReleaseDate);
}
