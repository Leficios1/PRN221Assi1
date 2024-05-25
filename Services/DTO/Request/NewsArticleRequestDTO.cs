using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO.Request
{
    public class NewsArticleRequestDTO
    {
        public string NewsArticleId { get; set; } = null!;

        public string? NewsTitle { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? NewsContent { get; set; }

        public short CategoryId { get; set; }

        public bool NewsStatus { get; set; }

        public short CreatedById { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public List<int>? Tags { get; set; }
    }
}
