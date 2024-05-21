﻿using Services.DTO.Request;
using Services.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interface
{
    public interface ISystemAccountServices
    {
        Task<bool> Login(string email, string password);
        Task<List<SystemAccountResponseDTO>> getAllAsync();
        Task<SystemAccountResponseDTO?> createAccount(SystemAccountRequestDTO dto);
        Task<SystemAccountResponseDTO?> updateAccount(SystemAccountRequestDTO dto);
        Task<bool> deleteAccount(int id);
    }
}
