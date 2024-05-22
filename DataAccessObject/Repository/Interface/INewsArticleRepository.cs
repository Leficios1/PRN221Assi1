using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject.Repository.Interface
{
    public interface INewsArticleRepository : IBaseRepository<NewsArticle>
    {
        public Task<List<NewsArticle>> GetAll();
        public Task<bool> getBySystemAccountId(int id);
        public Task<List<NewsArticle>> getByDate(DateTime startDate, DateTime endDate);
    }
}
