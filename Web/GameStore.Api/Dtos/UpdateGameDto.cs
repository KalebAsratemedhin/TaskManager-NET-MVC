using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class UpdateGameDto (
    [Required][MaxLength(50)] string Name,
    [Required][MaxLength(25)] string Genre,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);
