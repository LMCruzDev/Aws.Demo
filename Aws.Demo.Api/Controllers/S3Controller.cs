using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Aws.Demo.Api.Configuration;
using Aws.Demo.Api.Model.Files;
using Microsoft.AspNetCore.Mvc;

namespace Aws.Demo.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class S3Controller : ControllerBase
    {
        private readonly AmazonS3Client _amazonS3Client;

        public S3Controller(AwsConfiguration awsConfiguration)
        {
            _amazonS3Client = new AmazonS3Client(
                awsConfiguration.AccessKey,
                awsConfiguration.SecretKey,
                RegionEndpoint.EUWest2);
        }

        [HttpGet("files")]
        public async Task<IActionResult> GetFiles(string bucketName)
        {
            var response = await _amazonS3Client.ListObjectsAsync(new ListObjectsRequest
            {
                BucketName = bucketName
            });

            return Ok(response.S3Objects);
        }

        [HttpPost("files")]
        public async Task<IActionResult> PostFile(string bucketName, [FromBody] ApiAddFileRequest request)
        {
            var file = new FileInfo(request.FilePath);

            await _amazonS3Client.PutObjectAsync(new PutObjectRequest
            {
                InputStream = file.OpenRead(),
                BucketName = bucketName,
                Key = request.BucketFolderPath
            });

            return Ok();
        }

        [HttpDelete("files")]
        public async Task<IActionResult> DeleteFile(string bucketName, [FromBody] ApiDeleteFileRequest request)
        {
            var filePath = $"{request.FolderPath}/{request.File}";

            await _amazonS3Client.DeleteObjectAsync(new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = filePath
            });

            return Ok();
        }
    }
}