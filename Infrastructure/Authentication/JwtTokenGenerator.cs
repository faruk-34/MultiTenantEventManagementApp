using Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public string GenerateToken()
        {
            // Gizli anahtar
            var secretKey = "MySecretKey123 MySecretKey123 MySecretKey123 MySecretKey123 MySecretKey123";

            // Simetrik güvenlik anahtarını oluştur
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // İmza algoritmasını belirt
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            // Token detaylarını oluştur
            var securityToken = new JwtSecurityToken(
                issuer: "Faruk Kalkan",
                audience: "Mulakat",
                expires: DateTime.UtcNow.AddHours(1), // Token geçerlilik süresi
                signingCredentials: signingCredentials
            );

            // Token'ı oluştur
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(securityToken);
        }
    }


}
