using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgrnPoddLib.EgrnClient.Data
{
    public class RequestInfo
    {
        public DateTime? CreatedAt { set; get; }
        public string? QueryId { set; get; }
        public bool? IsSuccess { set; get; }
        public string? Error { set; get; }
    }
}
