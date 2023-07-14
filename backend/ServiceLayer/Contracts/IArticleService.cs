using ServiceLayer.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Contracts
{
    public interface IArticleService
    {

        IEnumerable<ArticleDto> GetAll();

        ArticleDto? Get(int id);

        void Delete(int entityId);

        void Insert(ArticleDto entity);

        void Update(int id, ArticleDto entity);
    }
}
