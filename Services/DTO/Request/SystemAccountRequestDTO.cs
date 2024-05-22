using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO.Request
{
    public class SystemAccountRequestDTO
    {
        public short AccountId { get; set; }
        public string AccountName { get; set; } = null!;

        public string AccountEmail { get; set; } = null!;

        public int AccountRole { get; set; }

        public string AccountPassword { get; set; } = null!;
    }
}
