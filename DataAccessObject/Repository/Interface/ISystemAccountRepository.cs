using BusinessObject.Model;
using DataAccessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject.Repository.Interface
{
    public interface ISystemAccountRepository : IBaseRepository<SystemAccount>
    {
        //public Task<bool> GetEmailAndPassword(string email, string password);
        public Task<List<SystemAccount>> GetAll();
        public Task<SystemAccount> Create(SystemAccount systemAccount);
        public Task<SystemAccount> UpdateAsync(SystemAccount systemAccount);
        public Task<bool> DeleteAsync (SystemAccount systemAccount);

    }   
}
