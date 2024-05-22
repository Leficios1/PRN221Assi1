using BusinessObject.Model;
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
    }
}
