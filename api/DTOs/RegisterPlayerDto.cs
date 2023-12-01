namespace api.DTOs;

public record RegisterPlayerDto(
    [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage = "Bad Email Format.")] string Email,
    string Name,
    string LastName,
    [MinLength(10), MaxLength(10)]string NationalCode,
    int Height,
    int Age,
    [DataType(DataType.Password), MinLength(7), MaxLength(20 )]string Password
);

public record LoginDto(
      [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage = "Bad Email Format.")] string Email,
      [DataType(DataType.Password), MinLength(7), MaxLength(20)] string Password
);

public record LoggedInDto(
     string Id,
     string Email,
     string Name,
     string LastName,
     string NationalCode,
     int Height,
     int Age,
     string Token
);