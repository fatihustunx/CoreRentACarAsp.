using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Conceretes
{
    public class Payment : IEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CardNo { get; set; }
        public short Expiration { get; set; }
        public decimal Balance { get; set; }
        public int CvcCode { get; set; }
    }
}