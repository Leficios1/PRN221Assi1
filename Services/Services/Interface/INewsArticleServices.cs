using BusinessObject.Model;
using Services.DTO.Request;
using Services.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interface
{
    public interface INewsArticleServices
    {
        Task<List<NewsArticleResponseDTO>> createReport(DateTime startDate, DateTime endDate);
        Task<List<NewsArticleResponseDTO>> getAll();
        Task<List<NewsArticleResponseDTO>> getByAccountId(short id);
        Task<bool> createNewsArticle(NewsArticleRequestDTO dto);
        Task<List<Tag>> GetTagsByIds(List<int> tagIds);
        Task UpdateNewsArticleAsync(NewsArticleRequestDTO dto);
        Task<string> getCreateNameByCreateId(short? Id);
        Task<List<Category>> GetAllCategories();
        Task<NewsArticle?> getById(string id);
    }
}
