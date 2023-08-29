using System;
using System.Text.Json.Serialization;

namespace ServiceLayer.DtoModels
{
	public class TargetDto
	{
		[JsonConstructor]
		public TargetDto() { }

		public TargetDto(int uId, int tId)
		{
			userId = uId;
			targetId = tId;
		}

		public int userId { get; set; }
		public int targetId { get; set; }
	}
}

