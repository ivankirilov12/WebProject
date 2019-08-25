using Google.Apis.Drive.v3;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace PcPartPicker.Services.Interfaces
{
    public interface IGoogleDriveService
    {
        void DownloadFile(string blobId, string savePath);

        string UploadFile(IFormFile formFile);

        void DeleteFile(string id);
    }
}
