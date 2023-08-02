using DomainLayer.Models;
using RepositoryLayer;
using ServiceLayer.Contracts;

namespace ServiceLayer.Services
{
    public class FilterService : IFilterService
    {
        private readonly IRepository<User> _userRepository;
        public FilterService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public DateTime? ParsePostedDate(string posted)
        {
            switch (posted)
            {
                case "last-24h":
                    return DateTime.Now.AddDays(-1);

                case "last-3-days":
                    return DateTime.Now.AddDays(-3);

                case "last-7-days":
                    return DateTime.Now.AddDays(-7);

                case "last-14-days":
                    return DateTime.Now.AddDays(-14);

                default:
                    return null;
            }
        }

        public DateTime? ParseExpirationDate(string expiration)
        {
            switch (expiration)
            {
                case "end-of-week":
                    return DateTime.Now.AddDays(DayOfWeek.Saturday - DateTime.Now.DayOfWeek);


                case "in-1-month":
                    return DateTime.Now.AddMonths(1);


                case "in-3-months":
                    return DateTime.Now.AddMonths(3);

                default:
                    return null;
            }
        }

        public bool IsAuthorMatch(Article article, string authors)
        {
            if (authors.ToLower() != "all")
            {
                var creatorName = _userRepository.Get(article.CreatorId);
                return creatorName == null ? false : Array.Exists(authors.Split(","), element => element == article.Author);
            }
            return true;

        }

        public bool IsCategoryMatch(Article article, string category)
        {
            return (category.ToLower() == "all") || article.Category.ToLower() == category.ToLower();
        }

        public bool IsCityMatch(Article article, string city)
        {
            return (city.ToLower() == "all") || article.City.ToLower() == city.ToLower();
        }

        public bool IsStoreMatch(Article article, string store)
        {
            return (store.ToLower() == "all") || article.Store.ToLower() == store.ToLower();
        }

        public bool IsPostedMatch(Article article, string posted)
        {
            DateTime? postedDateTime = ParsePostedDate(posted);
            if (postedDateTime == null)
            {
                return true;
            }

            return article.Posted.Date >= postedDateTime.Value;
        }

        public bool IsExpirationMatch(Article article, string expiration)
        {
            DateTime? expirationDateTime = ParseExpirationDate(expiration);
            if (expirationDateTime == null)
            {
                return true;
            }

            return article.Expiration.Date <= expirationDateTime.Value;
        }

    }
}
