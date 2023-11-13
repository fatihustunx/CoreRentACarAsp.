using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Requests
{
    public class PayOfRent
    {
        public int RentId { get; set; }
        public string FullName { get; set; }
        public string CardNo { get; set; }
        public short Expiration { get; set; }
        public int CvcCode { get; set; }
    }
}