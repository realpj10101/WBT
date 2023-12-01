namespace api.DTOs;

public record CoachDto(
    string Id,
    string Email,
    string Name,
    string LastName,
    string NationalCode,
    int Age,
    string Record
);
