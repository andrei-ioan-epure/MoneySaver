using DomainLayer.Models;

namespace ServiceLayer.Contracts
{
    public interface IFilterService
    {
        DateTime? ParsePostedDate(string posted);
        DateTime? ParseExpirationDate(string expiration);
        bool IsAuthorMatch(Article article, string authors);
        bool IsCategoryMatch(Article article, string category);
        bool IsCityMatch(Article article, string city);
        bool IsStoreMatch(Article article, string store);
        bool IsPostedMatch(Article article, string posted);
        bool IsExpirationMatch(Article article, string expiration);


    }
}
