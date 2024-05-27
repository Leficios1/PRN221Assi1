using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject.Repository.Interface
{
    public interface ITagRepository : IBaseRepository<Tag>
    {
        Task<List<Tag>> GetAll();
    }
}
