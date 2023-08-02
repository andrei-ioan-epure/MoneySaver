using DomainLayer.Models;
using ServiceLayer.DtoModels;

namespace ServiceLayer.Contracts
{
	public interface IJwtUtils
	{
        public string CreateToken(User user);
        public JwtDto? ValidateToken(string token);

    }
}

