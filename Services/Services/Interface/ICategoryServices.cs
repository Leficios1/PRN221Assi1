using BusinessObject.Model;
using Services.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interface
{
    public interface ICategoryServices
    {
        Task<List<Category>> getAllAsync();
        Task<Category?> createCategory(CategoryRequestDTO dto);
        Task<Category> updateCategory(CategoryUpdateRequestDTO dto);
        Task<bool> deleteCategory(short Id);
        Task<short> getCategoryIdByCaetegoryName(string name);
    }
}
