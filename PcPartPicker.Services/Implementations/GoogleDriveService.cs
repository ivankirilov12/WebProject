using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Http;
using PcPartPicker.Services.Interfaces;
using System;
using System.IO;
using System.Threading;

namespace PcPartPicker.Services.Implementations
{
    public class GoogleDriveService : IGoogleDriveService
    {
        private string[] Scopes = { DriveService.Scope.Drive };
        private string ApplicationName = "Part Picker";

        public void DeleteFile(string id)
        {
            if (id != null)
            {
                try
                {
                    var service = GetDriveServiceInstance();
                    service.Files.Delete(id).Execute();
                }
                catch (Exception e)
                {
                    //file couldn't be deleted
                }
            }            
        }

        public void DownloadFile(string blobId, string savePath)
        {
            var service = GetDriveServiceInstance();
            var request = service.Files.Get(blobId);
            var stream = new MemoryStream();
            // Add a handler which will be notified on progress changes.
            // It will notify on each chunk download and when the
            // download is completed or failed.
            request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
            {
                switch (progress.Status)
                {
                    case Google.Apis.Download.DownloadStatus.Downloading:
                        {
                            Console.WriteLine(progress.BytesDownloaded);
                            break;
                        }
                    case Google.Apis.Download.DownloadStatus.Completed:
                        {
                            Console.WriteLine("Download complete.");
                            SaveStream(stream, savePath);
                            break;
                        }
                    case Google.Apis.Download.DownloadStatus.Failed:
                        {
                            Console.WriteLine("Download failed.");
                            break;
                        }
                }
            };
            request.Download(stream);
        }

        public string UploadFile(IFormFile formFile)
        {
            var service = GetDriveServiceInstance();
            var fileMetadata = new Google.Apis.Drive.v3.Data.File();
            fileMetadata.Name = formFile.Name;
            fileMetadata.MimeType = "image/jpeg";            
            FilesResource.CreateMediaUpload request;
            using (var stream = formFile.OpenReadStream())
            {
                request = service.Files.Create(fileMetadata, stream, "image/jpeg");
                request.Fields = "id";
                request.Upload();
            }

            var file = request.ResponseBody;

            return file.Id;
        }

        private DriveService GetDriveServiceInstance()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                credPath = Path.Combine(credPath, "./credentials/credentials.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }

        private void SaveStream(MemoryStream stream, string saveTo)
        {
            using (FileStream file = new FileStream(saveTo, FileMode.Create, FileAccess.Write))
            {
                stream.WriteTo(file);
            }
        }
    }
}
