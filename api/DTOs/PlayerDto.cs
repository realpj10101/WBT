namespace api.DTOs;

public record PlayerDto(
    string Id,
    string Email,
    string Name,
    string LastName,
    string NationalCode,
    int Height,
    int Age
);
