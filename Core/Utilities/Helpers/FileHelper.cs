using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.Helpers
{


    public class FileHelper
    {
        public static class FileUploadHelper
        {
            public static readonly string UploadDir = "wwwroot";

            public static IDataResult<string> CreateImage(IFormFile formFile)
            {

                var imageExtension = Path.GetExtension(formFile.FileName);

                var imageFileName = Guid.NewGuid() + imageExtension;

                var imagePath = Path.Combine(UploadDir, imageFileName);

                using (var fs = new FileStream(imagePath, FileMode.Create))
                {
                    formFile.CopyTo(fs);

                    return new SuccessDataResult<string>(imagePath);
                }
            }

            public static IResult DeleteImage(string imagePath)
            {
                File.Delete(imagePath);

                return new SuccessResult();
            }

            public static IResult UpdateImage(string imagePath, IFormFile formFile)
            {

                using (var fs = new FileStream(imagePath, FileMode.Create))
                {
                    formFile.CopyTo(fs);

                    return new SuccessDataResult<string>(imagePath);
                }

                return new SuccessResult();
            }

        }
    }
}
