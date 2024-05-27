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
    public class TagServices : ITagServices
    {
        private readonly ITagRepository _tagRepository;

        public TagServices(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<List<Tag>> getAllAsync()
        {
            var db = await _tagRepository.GetAll();
            return db;
        }
    }
}
