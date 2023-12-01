using api.Models;

namespace api.Interfaces;
public interface ITokenService
{
    string CreateToken(Player player);
    string CreateToken(Coach coach);
}