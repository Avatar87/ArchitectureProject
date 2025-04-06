using AuthServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthServer.Providers
{
    public class JwtTokenProvider
    {
        private readonly string _tokenType;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _tokenLifetime;
        private readonly SymmetricSecurityKey _securityKey;

        protected JwtTokenProvider(
            string tokenType,
            string key,
            string issuer,
            string audience,
            int tokenLifetime)
        {
            _tokenType = tokenType;
            _issuer = issuer;
            _audience = audience;
            _tokenLifetime = tokenLifetime;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        public async Task<string> GenerateAsync(string purpose, UserManager<User> manager, User user, Guid gameId)
        {
            var creds = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256Signature);

            var roles = await manager.GetRolesAsync(user);
            var claims = await manager.GetClaimsAsync(user);
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            foreach (var role in roles) { claims.Add(new Claim(ClaimTypes.Role, role)); }

            var accessToken = CreateToken
            (
                creds,
                _issuer,
                _audience,
                claims,
                DateTime.UtcNow.AddMinutes(_tokenLifetime)
            );

            accessToken.SigningKey = _securityKey;
            accessToken.Payload["UserName"] = user.UserName;
            accessToken.Payload["GameId"] = gameId;

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(accessToken));
        }

        public Task<bool> ValidateAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(token))
            {
                return Task.FromResult(false);
            }

            try
            {
                ValidateTokenWithParams(token);
                return Task.FromResult(true);
            }
            catch (SecurityTokenExpiredException exception)
            {
                throw;
            }
            catch (SecurityTokenException e)
            {
                return Task.FromResult(false);
            }
        }

        protected JwtSecurityToken CreateToken(SigningCredentials credentials, string issuer, string audience, IEnumerable<Claim> claims, DateTime expires)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.CreateJwtSecurityToken(
                issuer: issuer,
                audience: audience,
                subject: new ClaimsIdentity(claims),
                notBefore: DateTime.UtcNow,
                expires: expires,
                signingCredentials: credentials
            );
        }


        private SecurityToken ValidateTokenWithParams(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = _securityKey,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    ClockSkew = TimeSpan.FromSeconds(5)
                },
                out SecurityToken validatedToken);

            return validatedToken;
        }
    }
}
