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
        public string ConnectionString { get; } // add option to override

        private readonly IConfiguration _configuration;
        private readonly CloudBlobClient _client;
        private readonly CloudBlobContainer _container;

        public FileUploadHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = configuration.GetConnectionString("StorageConnection");

            var account = CloudStorageAccount.Parse(ConnectionString);
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
