﻿using BusinessObject.Model;
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
    }
}