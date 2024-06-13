using BusinessObject.Model;
using DataAccessObject.Model;
using DataAccessObject.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject.Repository
{
    public class NewsArticleRepository : BaseRepository<NewsArticle>, INewsArticleRepository
    {
        private readonly FunewsManagementDbContext _context;

        public NewsArticleRepository(FunewsManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<NewsArticle>> GetAll()
        {
            var db = await _context.NewsArticles.ToListAsync();
            return db;
        }

        public async Task<List<NewsArticle>> getByDate(DateTime startDate, DateTime endDate)
        {
            var db = await _context.NewsArticles.Where(n => n.CreatedDate >= startDate && n.CreatedDate <= endDate)
                                                .OrderByDescending(n => n.CreatedDate)
                                                .ToListAsync();       
            return db;
        }

        public async Task<bool> getBySystemAccountId(int id)
        {
            var newsArticle = await _context.NewsArticles
                                            .Where(x => x.CreatedById == id)
                                            .FirstOrDefaultAsync();

            return newsArticle != null;
        }
    }
}
