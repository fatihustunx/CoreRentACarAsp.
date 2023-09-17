using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.AFiles
{
    public class FileOperations : IFileOperations
    {
        public string Upload(IFormFile formFile, string root)
        {

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            var Extension = Path.GetExtension(formFile.FileName);

            var guid = Guid.NewGuid().ToString();
            var filePath = ($"{guid}{Extension}");

            var path = ($"{root}{filePath}");

            using (var stream = File.Create(path))
            {
                formFile.CopyTo(stream);
                stream.Flush();

                return filePath;
            }
        }

        public string Update(IFormFile formFile, string filePath, string root)
        {
            var path = ($"{root}{filePath}");

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            return Upload(formFile, root);
        }

        public void Delete(string filePath, string root)
        {
            var path = ($"{root}{filePath}");

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}