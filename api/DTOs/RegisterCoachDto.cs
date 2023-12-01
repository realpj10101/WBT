namespace api.DTOs;
public record RegisterCoachDto(
    [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage = "Bad Email Format.")] string Email,
    string Name,
    string LastName,
    [MinLength(10), MaxLength(10)] string NationalCode,
    int Age,
    string Record,
    [DataType(DataType.Password), MinLength(7), MaxLength(20)] string Password
);

public record LoginCoachDto(
    [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage = "Bad Email Format.")] string Email,
    [DataType(DataType.Password), MinLength(7), MaxLength(20)] string Password
);

public record LoggedInCoachDto(
    string Id,
    string Email,
    string Name,
    string LastName,
    string NationalCode,
    int Age,
    string Record,
    string Token
);