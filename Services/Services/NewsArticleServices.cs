﻿using AutoMapper;
using BusinessObject.Model;
using DataAccessObject.Repository;
using DataAccessObject.Repository.Interface;
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

        public NewsArticleServices(INewsArticleRepository newsArticleRepository, ISystemAccountRepository systemAccountRepository,
                                    ICategoryRepository categoryRepository, IMapper mapper)
        {
            _newsArticleRepository = newsArticleRepository;
            _systemAccountRepository = systemAccountRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
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
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}