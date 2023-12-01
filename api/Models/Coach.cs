namespace api.Models;

public record Coach(
           [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
            string Email,
            string Name,
            string LastName,
            string NationalCode,
            int Age,
            string Record,
            byte[] PasswordSalt,
            byte[] PasswordHash
);