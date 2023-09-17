using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.AFiles
{
    public interface IFileOperations
    {
        string Upload(IFormFile formFile, string root);
        string Update(IFormFile formFile, string filePath
            , string root);
        void Delete(string filePath, string root);
    }
}