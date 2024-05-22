using BusinessObject.Model;
using DataAccessObject.Repository.Interface;
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

        public CategoryServices(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> getAllAsync()
        {
            try
            {
                var data = await _categoryRepository.GetAll();
                return data;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
