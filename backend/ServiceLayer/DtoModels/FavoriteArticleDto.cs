using System;
using System.Text.Json.Serialization;

namespace ServiceLayer.DtoModels
{
	public class FavoriteArticleDto
	{
        [JsonConstructor]
        public FavoriteArticleDto() { }

        public FavoriteArticleDto(int uId, int aId)
		{
			userId = uId;
			articleId = aId;
		}

		public int userId { get; set; }
		public int articleId { get; set; }
	}
}

