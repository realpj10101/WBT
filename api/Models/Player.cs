namespace api.Models;

public record Player(
       [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
          string Email,
          string Name,
          string LastName,
          string NationalCode,
          int Height,
          int Age,
          byte[] PasswordSalt,
          byte[] PasswordHash
);