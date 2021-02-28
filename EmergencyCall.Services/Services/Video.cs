using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EmergencyCall.Services.Services
{
    public class Video : IVideo
    {
        public async Task<string> UploadAsync(IFormFile file, string outerFolderName, string innerFolderName)
        {
            string folderPath = Path.Combine("wwwroot", outerFolderName, innerFolderName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            string fileExtension = file.ContentType.Substring(file.ContentType.IndexOf("/") + 1,
                                            file.ContentType.Length - file.ContentType.IndexOf("/") - 1);
            string fileName = Guid.NewGuid().ToString() + "_" + "berqa" + "." + fileExtension;
            string filePath = Path.Combine(folderPath, fileName);
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                await file.CopyToAsync(fileStream);
            return fileName;
        }
        public void Delete(string outerFolderName, string innerFolderName, string fileName)
        {
            string folderPath = Path.Combine("wwwroot", outerFolderName, innerFolderName);
            string filePath = Path.Combine(folderPath, fileName);
            if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);
        }
        public bool IsVideoValid(IFormFile file)
        {
            if (file.Length <= 50 * 1024 * 1024 && file.ContentType.Contains("video"))
                return true;
            else
                return false;
        }
    }
}
