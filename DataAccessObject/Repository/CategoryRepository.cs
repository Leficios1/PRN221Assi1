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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly FunewsManagementDbContext _context;

        public CategoryRepository(FunewsManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAll()
        {
            var data = await _context.Categories.ToListAsync();
            return data;
        }
    }
}
