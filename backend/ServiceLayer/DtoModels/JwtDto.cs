using System;
namespace ServiceLayer.DtoModels
{
	public class JwtDto
	{
		public JwtDto(int userID, string role)
		{
			UserID = userID;
			Role = role;
		}

		public int UserID { get; set; }
		public string Role { get; set; }
	}
}

