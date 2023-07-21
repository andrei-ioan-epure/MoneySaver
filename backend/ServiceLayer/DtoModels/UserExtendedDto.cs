using System;
namespace ServiceLayer.DtoModels
{
	public class UserExtendedDto : UserDto
	{
		public UserExtendedDto(int? id, string userName, string fullName, string email, string password, bool isCreator, List<ArticleDto> fav)
                : base(id, userName, fullName, email, password, isCreator)
		{
            FavoriteArticles = fav;
        }

        public List<ArticleDto> FavoriteArticles { get; set; }
    }
}

