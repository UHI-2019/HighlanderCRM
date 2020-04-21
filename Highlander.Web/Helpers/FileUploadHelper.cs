using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Highlander.Web.Helpers
{
    public class FileUploadHelper
    {
        private string _connectionString; // add option to override

        private readonly CloudBlobClient _client;
        private readonly CloudBlobContainer _container;

        public FileUploadHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("StorageConnection");

            CloudStorageAccount account = CloudStorageAccount.Parse(_connectionString);
            _client = account.CreateCloudBlobClient();
            _container = _client.GetContainerReference("highlander");
        }

        public byte[] ConvertImageToByteArray(IFormFile image)
        {
            byte[] result = null;

            using (var fileStream = image.OpenReadStream())
            {
                using (var memoryStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoryStream);
                    result = memoryStream.ToArray();
                }
                return result;
            }
        }

        public async Task<string> UploadImageByteArray(byte[] imageBytes, string fileName, string contentType)
        {
            if (imageBytes == null || imageBytes.Length == 0)
            {
                return null;
            }

            var cloudBlockBlob = _container.GetBlockBlobReference(fileName);
            cloudBlockBlob.Properties.ContentType = contentType;

            const int byteArrayStartIndex = 0;

            await cloudBlockBlob.UploadFromByteArrayAsync(
                imageBytes,
                byteArrayStartIndex,
                imageBytes.Length);

            var imageFullUrlPath = cloudBlockBlob.Uri.ToString();

            return imageFullUrlPath;
        }
    }
}
