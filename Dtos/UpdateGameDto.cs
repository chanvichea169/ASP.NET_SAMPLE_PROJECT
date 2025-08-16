namespace GameStore.Api.Dtos;

public record class UpdateGameDto
(
    int Id,
    String Name,
    String Gender,
    decimal Price,
    DateOnly ReleaseDate
);
