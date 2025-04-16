using Domain.Entities;

namespace Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Users user);
    }
}
