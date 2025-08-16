namespace GameStore.Api.Dtos;

public record class CreateGameDto(
    int Id,
    String Name,
    String Genre,
    decimal Price,
    DateOnly ReleaseDate
);
