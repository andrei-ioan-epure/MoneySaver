using System;
namespace ServiceLayer.DtoModels
{
	public class TokenDto
	{
		public TokenDto(string token, int id, string role, string fullName)
		{
			Token = token;
			Id = id;
			Role = role;
			FullName = fullName;
		}

		public string Token { get; set; }
		public int Id { get; set; }
		public string Role { get; set; }
		public string FullName { get; set; }
	}
}

