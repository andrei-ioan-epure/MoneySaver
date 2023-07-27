using System;
using DomainLayer.Models;

namespace ServiceLayer.Contracts
{
	public interface IJwtUtils
	{
        public string CreateToken(User user);
        public int? ValidateToken(string token);

    }
}

