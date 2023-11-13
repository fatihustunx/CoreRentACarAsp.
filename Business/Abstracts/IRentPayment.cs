using Core.Utilities.Results;
using Entities.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IRentPaymentService
    {
        IResult PayOfRent(PayOfRent payOfRent);
    }
}
