using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Aws.Demo.Api.Business.Abstractions;
using Aws.Demo.Api.Configuration;
using Microsoft.AspNetCore.Http;

namespace Aws.Demo.Api.Business
{
    public class AmazonS3Service : IAmazonS3Service
    {
        private readonly AmazonS3Client _amazonS3Client;

        public AmazonS3Service(AwsConfiguration awsConfiguration)
        {
            _amazonS3Client = new AmazonS3Client(
                awsConfiguration.AccessKey,
                awsConfiguration.SecretKey,
                RegionEndpoint.EUWest2);
        }

        public async Task CreateBucketAsync(string bucketName)
        {
            if (!(await AmazonS3Util.DoesS3BucketExistV2Async(_amazonS3Client, bucketName)))
            {
                var putBucketRequest = new PutBucketRequest
                {
                    BucketName = bucketName,
                    UseClientRegion = true
                };

                _ = await _amazonS3Client.PutBucketAsync(putBucketRequest);
            }
        }

        public async Task AddFileAsync(string bucketName, string folderPath, IFormFile file)
        {
            await CreateBucketAsync(bucketName);

            if (file.Length > 0)
            {
                await _amazonS3Client.PutObjectAsync(new PutObjectRequest
                {
                    InputStream = file.OpenReadStream(),
                    BucketName = bucketName,
                    Key = folderPath
                });
            }
        }
    }
}
