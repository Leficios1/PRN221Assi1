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
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        private readonly FunewsManagementDbContext _context;

        public TagRepository(FunewsManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Tag>> GetAll()
        {
            var db = await _context.Tags.ToListAsync();
            return db;
        }
    }
}
