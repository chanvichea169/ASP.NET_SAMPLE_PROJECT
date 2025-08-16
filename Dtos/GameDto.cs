using System;

namespace GameStore.Api.Dtos;

public record class GameDto(
    int Id,
    String Name,
    String Gender,
    decimal Price,
    DateOnly ReleaseDate
);