namespace GameStore.Api.Dtos;

public record class UpdateGameDto
(
    int Id,
    String Name,
    String Genre,
    decimal Price,
    DateOnly ReleaseDate
);
