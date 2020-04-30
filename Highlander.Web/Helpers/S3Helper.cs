using System;
using System.IO;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
namespace Highlander.Web.Helpers
{
    public class S3Helper
    {

        private readonly string accessKeyID = Credential.AmazonS3AccessKeyID;
        private readonly string secretKey = Credential.AmazonS3Secret;
        private readonly string bucketName = Credential.AmazonBucket;

        public Boolean UploadFile(IFormFile file, string filename = "PickFileNameFromSelf")
        {
            if(filename == "PickFileNameFromSelf")
            {
                filename = file.FileName;
            }

            using (var client = new AmazonS3Client(accessKeyID, secretKey, RegionEndpoint.EUWest2))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = filename,
                        BucketName = bucketName,
                        CannedACL = S3CannedACL.BucketOwnerFullControl
                    };

                    var fileTransferUtility = new TransferUtility(client);
                    fileTransferUtility.Upload(uploadRequest);
                    
                }
            }
            return true;
        }

        public string GetFilePath(string filename)
        {
            var URL = "https://highlandermuseumcrm.s3.eu-west-2.amazonaws.com/"+filename;
            return URL;
        }

        public Boolean DeleteFile(string filename)
        {

            using (var client = new AmazonS3Client(accessKeyID, secretKey, RegionEndpoint.EUWest2))
            {
                client.DeleteObjectAsync(new Amazon.S3.Model.DeleteObjectRequest()
                {
                    BucketName = bucketName,
                    Key = filename
                });
            }

            return true;
        }
    }
}