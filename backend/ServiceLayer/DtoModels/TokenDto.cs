using System;
namespace ServiceLayer.DtoModels
{
	public class TokenDto
	{
		public TokenDto(string token, int id, string role)
		{
			Token = token;
			Id = id;
			Role = role;
		}

		public string Token { get; set; }
		public int Id { get; set; }
		public string Role { get; set; }
	}
}

