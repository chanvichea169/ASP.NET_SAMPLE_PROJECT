namespace GameStore.Api.Dtos;

public record class CreateGameDto(
    int Id,
    String Name,
    String Gender,
    decimal Price,
    DateOnly ReleaseDate
);
