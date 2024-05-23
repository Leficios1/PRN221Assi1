using AutoMapper;
using BusinessObject.Model;
using Services.DTO.Request;
using Services.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapper
{
    public class MappingEntities : Profile
    {
        public MappingEntities() 
        {
            CreateMap<SystemAccountResponseDTO, SystemAccount>().ReverseMap();
            CreateMap<SystemAccountRequestDTO, SystemAccount>().ReverseMap();
            CreateMap<NewsArticleResponseDTO, NewsArticle>().ReverseMap();
            CreateMap<CategoryRequestDTO, Category>().ReverseMap();
            CreateMap<CategoryUpdateRequestDTO, Category>().ReverseMap();
        }
    }
}
