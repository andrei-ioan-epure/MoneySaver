using System;
using DomainLayer.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ServiceLayer.Contracts;
using ServiceLayer.DtoModels;

namespace ServiceLayer.Services
{
	public class JwtUtils : IJwtUtils
	{
		public JwtUtils()
		{
		}

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.UserName),
                new Claim("role", user.IsCreator? "admin" : "user")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("CeaMaiComplicataCheie"));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddYears(1),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public JwtDto? ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("CeaMaiComplicataCheie");

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                string isCreator = jwtToken.Claims.First(x => x.Type == "role").Value;

                JwtDto jwtDto = new JwtDto(userId, isCreator);

                return jwtDto;
            }
            catch
            {
                return null;
            }
        }
    }
}

