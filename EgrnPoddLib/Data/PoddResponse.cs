using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgrnPoddLib.Data
{
    public class PoddResponse
    {
        public HttpContent Data { get; set; }
        public async Task<string> getContentAsStringAsync() => await Data.ReadAsStringAsync();
    }
}
