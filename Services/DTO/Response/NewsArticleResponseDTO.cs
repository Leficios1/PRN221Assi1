﻿using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO.Response
{
    public class NewsArticleResponseDTO
    {
        public string NewsArticleId { get; set; } = null!;

        public string? NewsTitle { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? NewsContent { get; set; }

        public string CategoryName { get; set; } = null!;

        public string NewsStatus { get; set; } = null!;

        public string CreatedBy { get; set; } = null!;

        public DateTime? ModifiedDate { get; set; }
        public List<Tag>? Tags { get; set; } 
    }
}
