using AutoMapper;
using BusinessObject.Model;
using DataAccessObject.Repository;
using DataAccessObject.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Services.DTO.Request;
using Services.DTO.Response;
using Services.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class NewsArticleServices : INewsArticleServices
    {
        private readonly INewsArticleRepository _newsArticleRepository;
        private readonly ISystemAccountRepository _systemAccountRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Tag> _tagRepository;
        public NewsArticleServices(INewsArticleRepository newsArticleRepository, ISystemAccountRepository systemAccountRepository,
                                    ICategoryRepository categoryRepository, IMapper mapper, IBaseRepository<Tag> tagRepository)
        {
            _newsArticleRepository = newsArticleRepository;
            _systemAccountRepository = systemAccountRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _tagRepository = tagRepository;
        }

        public async Task<bool> createNewsArticle(NewsArticleRequestDTO dto)
        {
            try
            {
                var data = await _newsArticleRepository.GetById(dto.NewsArticleId);
                if (data != null)
                {
                    return false;
                }
                var mapper = _mapper.Map<NewsArticle>(dto);
                if (dto.Tags != null && dto.Tags.Any())
                {
                    var tag = await _tagRepository.Get().Where(t => dto.Tags.Contains(t.TagId)).ToListAsync();
                    if (tag != null)
                    {
                        mapper.Tags = tag;
                    }
                }
                await _newsArticleRepository.AddAsync(mapper);
                await _newsArticleRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<NewsArticleResponseDTO>> createReport(DateTime startDate, DateTime endDate)
        {
            try
            {
                var data = await _newsArticleRepository.getByDate(startDate, endDate);
                var reportData = new List<NewsArticleResponseDTO>();
                foreach (var news in data)
                {
                    var dto = _mapper.Map<NewsArticleResponseDTO>(news);
                    var accountInfo = await _systemAccountRepository.GetById(news.CreatedById);
                    dto.CreatedBy = accountInfo.AccountName;
                    var categoryInfo = await _categoryRepository.GetById(news.CategoryId ?? 0);
                    dto.CategoryName = categoryInfo.CategoryName;

                    reportData.Add(dto);
                }
                if (!data.Any())
                {
                    return null;
                }
                return reportData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<NewsArticleResponseDTO>> getAll()
        {
            try
            {
                var data = await _newsArticleRepository.GetAll();
                var result = new List<NewsArticleResponseDTO>();
                foreach (var item in data)
                {
                    var dto = _mapper.Map<NewsArticleResponseDTO>(item);
                    var accountInfo = await _systemAccountRepository.GetById(item.CreatedById);
                    dto.CreatedBy = accountInfo.AccountName;
                    var categoryInfo = await _categoryRepository.GetById(item.CategoryId);
                    dto.CategoryName = categoryInfo.CategoryName;
                    if (item.NewsStatus == true)
                    {
                        dto.NewsStatus = "Active";
                    }
                    else
                    {
                        dto.NewsStatus = "InActive";
                    }
                    var tag = await _tagRepository.Get().Where(t => t.NewsArticles.Any(n => n.NewsArticleId == item.NewsArticleId)).ToListAsync();
                    dto.Tags = tag;
                    result.Add(dto);
                }
                if (!data.Any())
                {
                    return null;
                }
                return result.OrderBy(x => x.NewsArticleId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<NewsArticleResponseDTO>> getByAccountId(short id)
        {
            try
            {
                var data = await _newsArticleRepository.Get().Where(x => x.CategoryId == id).ToListAsync();
                var result = new List<NewsArticleResponseDTO>();
                if (!data.Any())
                {
                    return null;
                }
                foreach (var item in data)
                {
                    var mapper = _mapper.Map<NewsArticleResponseDTO>(item);
                    var categoryInfo = await _categoryRepository.GetById(item.CategoryId);
                    mapper.CategoryName = categoryInfo.CategoryName;
                    result.Add(mapper);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                var data = await _categoryRepository.GetAll();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> getCreateNameByCreateId(short? Id)
        {
            try
            {
                var data = await _systemAccountRepository.GetById(Id);
                if (data != null)
                {
                    return data.AccountName;
                }
                else
                {
                    return "Not Found ID Account";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Tag>> GetTagsByIds(List<int> tagIds)
        {
            return await _tagRepository.Get().Where(t => tagIds.Contains(t.TagId)).ToListAsync();
        }
        public async Task UpdateNewsArticleAsync(NewsArticleRequestDTO dto)
        {
            var newsArticle = await _newsArticleRepository.GetById(dto.NewsArticleId);
            if (newsArticle == null)
            {
                throw new Exception("News Article not found");
            }
            newsArticle.NewsTitle = dto.NewsTitle;
            newsArticle.NewsContent = dto.NewsContent;
            newsArticle.CategoryId = dto.CategoryId;
            newsArticle.NewsStatus = dto.NewsStatus;
            newsArticle.ModifiedDate = dto.ModifiedDate;
            var existingTags = await _tagRepository.Get()
                .Where(t => t.NewsArticles.Any(n => n.NewsArticleId == newsArticle.NewsArticleId))
                .ToListAsync();

            var taginDTO = dto.Tags.ToList();

            //This is tag need to check Update
            var tagNeedToCheck = taginDTO.Except(existingTags.Select(t => t.TagId)).ToList();

            //This is tag to remove
            var tagToRemove = existingTags.Where(e => !taginDTO.Contains(e.TagId)).ToList();

            foreach (var tag in tagNeedToCheck)
            {
                var flag = await _tagRepository.Get().SingleOrDefaultAsync(t => t.TagId == tag);
                if(flag != null)
                {
                    newsArticle.Tags.Add(flag);
                }
            }
            foreach(var tag in tagToRemove)
            {
                var tag1 = newsArticle.Tags.SingleOrDefault(t => t.TagId == tag.TagId);
                if (tag1 != null)
                {
                    newsArticle.Tags.Remove(tag);
                }
            }
            _newsArticleRepository.Update(newsArticle);
            await _newsArticleRepository.SaveChangesAsync();
        }

        public async Task<NewsArticle?> getById(string id)
        {
            try
            {
                var data = await _newsArticleRepository.GetById(id);
                if (data == null)
                {
                    return null;
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}", ex);
            }
        }

        public async Task<bool> deleteNewsArticle(string id)
        {
            try
            {
                var data = await _newsArticleRepository.GetById(id);
                if (data == null)
                {
                    return false;
                }
                else
                {
                    _newsArticleRepository.Delete(data);
                    await _newsArticleRepository.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<NewsArticleResponseDTO>> search(string? data)
        {
            try
            {
                var result = await _newsArticleRepository.Get().Where(n => n.NewsContent.ToUpper().Contains(data.ToUpper())
                                   || n.NewsTitle.ToUpper().Contains(data.ToUpper())).ToListAsync();

                var response = new List<NewsArticleResponseDTO>();
                foreach (var item in result)
                {
                    var dto = _mapper.Map<NewsArticleResponseDTO>(item);
                    var accountInfo = await _systemAccountRepository.GetById(item.CreatedById);
                    dto.CreatedBy = accountInfo.AccountName;
                    var categoryInfo = await _categoryRepository.GetById(item.CategoryId);
                    dto.CategoryName = categoryInfo.CategoryName;
                    if (item.NewsStatus == true)
                    {
                        dto.NewsStatus = "Active";
                    }
                    else
                    {
                        dto.NewsStatus = "InActive";
                    }
                    var tag = await _tagRepository.Get().Where(t => t.NewsArticles.Any(n => n.NewsArticleId == item.NewsArticleId)).ToListAsync();
                    dto.Tags = tag;
                    response.Add(dto);
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
