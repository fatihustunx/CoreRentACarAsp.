using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class Rules
    {
        public static List<IResult> Run(params IResult[] logics)
        {
            List<IResult> errorResults = new List<IResult>();
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    errorResults.Add(logic);
                }
            }

            return errorResults;
        }

    }
}