using System.ComponentModel.DataAnnotations;

namespace MyTestDotnetApp.Dtos
{
    public record class UpdateGameDto(
        [Required][StringLength(50)] string Name,
        [Required][StringLength(20)] string Genre,
        [Range(1, 1000)] decimal Price,
        DateOnly ReleaseDate
    );
}
