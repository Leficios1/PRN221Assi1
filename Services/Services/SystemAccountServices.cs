using AutoMapper;
using BusinessObject.Model;
using DataAccessObject.Repository;
using DataAccessObject.Repository.Interface;
using Services.DTO.Request;
using Services.DTO.Response;
using Services.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class SystemAccountServices : ISystemAccountServices
    {
        private readonly ISystemAccountRepository _systemAccountRepository;
        private readonly INewsArticleRepository _newsArticleRepository;
        private readonly IMapper _mapper;

        public SystemAccountServices(ISystemAccountRepository systemAccountRepository, IMapper mapper, INewsArticleRepository newsArticleRepository)
        {
            _systemAccountRepository = systemAccountRepository;
            _newsArticleRepository = newsArticleRepository;
            _mapper = mapper;
        }

        public async Task<SystemAccountResponseDTO?> createAccount(SystemAccountRequestDTO dto)
        {
            try
            {
                var account = await _systemAccountRepository.GetAll();
                var flag = account.Where(x => x.AccountEmail.Equals(dto.AccountEmail) && x.AccountPassword.Equals(dto.AccountPassword));
                if (flag.Any())
                {
                    return null;
                }
                var map = _mapper.Map<SystemAccount>(dto);
                var createAccount = await _systemAccountRepository.Create(map);
                var result = _mapper.Map<SystemAccountResponseDTO>(createAccount);
                return result;

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> deleteAccount(short id)
        {
            try
            {
                var account = await _systemAccountRepository.GetById(id);
                if(account == null)
                {
                    throw new Exception($"Account {id} does not exist");
                }
                if(await _newsArticleRepository.getBySystemAccountId(account.AccountId))
                {
                    return false;
                }
                await _systemAccountRepository.DeleteAsync(account);
                return true;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> getAccountName(string email)
        {
            var data = await _systemAccountRepository.FindOne(x => x.AccountEmail.Equals(email));
            return data.AccountName;
        }

        public async Task<List<SystemAccountResponseDTO>> getAllAsync()
        {
            try
            {
                var account = await _systemAccountRepository.GetAll();
                var map = _mapper.Map<List<SystemAccountResponseDTO>>(account);
                return map;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Login(string email, string password)
        {
            try
            {
                var account = await _systemAccountRepository.GetAll();
                var flag = account.Where(x => x.AccountEmail.Equals(email) && x.AccountPassword.Equals(password));
                if (!flag.Any())
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SystemAccountResponseDTO?> updateAccount(SystemAccountRequestDTO dto)
        {
            try
            {
                var account = await _systemAccountRepository.GetById(dto.AccountId);
                if (account == null)
                {
                    return null;
                }
                _mapper.Map(dto, account);
                await _systemAccountRepository.UpdateAsync(account);
                await _systemAccountRepository.SaveChangesAsync();
                
                var result = _mapper.Map<SystemAccountResponseDTO>(account);
                return result;

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
