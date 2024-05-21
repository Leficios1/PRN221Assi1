using BusinessObject.Model;
using DataAccessObject.Model;
using DataAccessObject.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject.Repository
{
    public class SystemAccountRepository : BaseRepository<SystemAccount>, ISystemAccountRepository
    {
        private readonly FunewsManagementDbContext _context;

        public SystemAccountRepository(FunewsManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SystemAccount> Create(SystemAccount systemAccount)
        {
            _context.Add(systemAccount);
            await _context.SaveChangesAsync();
            return systemAccount;
        }

        public async Task<bool> Delete(SystemAccount systemAccount)
        {
            Delete(systemAccount);
            await SaveChangesAsync();
            return true;
        }

        //public async Task<bool> GetEmailAndPassword(string email, string password)
        //{
        //    try
        //    {
        //        var flag = await _context.SystemAccounts.FirstOrDefaultAsync(x => x.AccountEmail.Equals(email) && x.AccountPassword.Equals(password));
        //        return flag != null;
        //    }catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public async Task<List<SystemAccount>> GetAll()
        {
            var db =await _context.SystemAccounts.ToListAsync();
            return db;
        }

        public async Task<SystemAccount> Update(SystemAccount systemAccount)
        {
            Update(systemAccount);
            await SaveChangesAsync();
            return systemAccount;
        }
    }
}
