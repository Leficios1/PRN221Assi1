using AutoMapper;
using BusinessObject.Model;
using DataAccessObject.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Services.DTO.Request;
using Services.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly INewsArticleRepository _newsArticleRepository;
        private readonly IMapper _mapper;

        public CategoryServices(ICategoryRepository categoryRepository, IMapper mapper, INewsArticleRepository newsArticleRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _newsArticleRepository = newsArticleRepository;
        }

        public async Task<Category?> createCategory(CategoryRequestDTO dto)
        {
            try
            {
                var mapper = _mapper.Map<Category>(dto);
                await _categoryRepository.AddAsync(mapper);
                await _categoryRepository.SaveChangesAsync();
                return mapper;
            }
            catch (DbUpdateException dbEx)
            {
                var message = $"DbUpdateException: {dbEx.Message}, InnerException: {dbEx.InnerException?.Message}";
                throw new Exception(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> deleteCategory(short Id)
        {
            try
            {
                var data = await _categoryRepository.GetById(Id);
                if (data == null)
                {
                    throw new Exception("Not Found ID");
                }
                else
                {
                    var flag = await _newsArticleRepository.Get().Where(x => x.CategoryId == Id).SingleOrDefaultAsync();
                    if (flag == null)
                    {
                        _categoryRepository.Delete(data);
                        await _categoryRepository.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                var message = $"DbUpdateException: {dbEx.Message}, InnerException: {dbEx.InnerException?.Message}";
                throw new Exception(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Category>> getAllAsync()
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

        public async Task<short> getCategoryIdByCaetegoryName(string name)
        {
            try
            {
                var data = await _categoryRepository.Get().Where(c => c.CategoryName.Equals(name)).SingleOrDefaultAsync();
                return data.CategoryId;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> updateCategory(CategoryUpdateRequestDTO dto)
        {
            try
            {
                var data = await _categoryRepository.GetById(dto.CategoryId);
                _mapper.Map(data, dto);
                _categoryRepository.Update(data);
                await _categoryRepository.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
