using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Requests
{
    public class PaymentForPay
    {
        public string FullName { get; set; }
        public string CardNo { get; set; }
        public short Expiration { get; set; }
        public decimal TotalCost { get; set; }
        public int CvcCode { get; set; }
    }
}
