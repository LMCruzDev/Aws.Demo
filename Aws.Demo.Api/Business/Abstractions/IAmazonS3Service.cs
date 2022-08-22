using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Aws.Demo.Api.Business.Abstractions
{
    public interface IAmazonS3Service
    {
        Task CreateBucketAsync(string bucketName);

        Task AddFileAsync(string bucketName, string folderPath, IFormFile file);
    }
}
