using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aws.Demo.Api.Business.Abstractions;
using Aws.Demo.Api.Business.Mappers;
using Aws.Demo.Api.Data.Abstraction;
using Aws.Demo.Api.Messaging.Abstraction;
using Aws.Demo.Api.Messaging.Model;
using Aws.Demo.Api.Model.Forms;
using Google.Apis.Util;
using Microsoft.AspNetCore.JsonPatch;

namespace Aws.Demo.Api.Business
{
    public class PdfService : IPdfService
    {
        private readonly IRepository<Data.Model.DataFormsPdf, string, string> _repository;
        private readonly IPublisher<FormPdfMessage> publisher;

        public PdfService(
            IRepository<Data.Model.DataFormsPdf, string, string> repository,
            IPublisher<FormPdfMessage> publisher)
        {
            _repository = repository;
            this.publisher = publisher;
        }

        public async Task<List<ApiFormsPdf>> ListAsync(Guid id)
        {
            var formsPdfs = await _repository.ListAsync(id.ToString());
            
            return formsPdfs.ToModel();
        }

        public async Task<ApiFormsPdf> GetByIdAsync(Guid id, int version)
        {
            var result = await _repository.GetByIdAsync(id.ToString(), version.ToString());
            
            return result.ToModel();
        }

        public async Task<ApiFormsPdf> AddAsync(ApiAddFormsPdf apiAddModel)
        {
            apiAddModel.ThrowIfNull(nameof(apiAddModel));

            var model = apiAddModel.ToModel();

            var pdfModels = await ListAsync(apiAddModel.Id);

            if(pdfModels.Any())
            {
                var pdfModel = pdfModels.OrderByDescending(v => v.Version).FirstOrDefault();
                model.Version = pdfModel.Version + 1;
            }

            await _repository.SaveAsync(model.ToData());

            var updatedModel = await this.GetByIdAsync(apiAddModel.Id, model.Version);

            await this.publisher.Publish(apiAddModel.ToMessage());

            return model;
        }

        public async Task UpdateAsync(JsonPatchDocument<ApiFormsPdf> patchDocument, Guid id, int version)
        {
            patchDocument.ThrowIfNull(nameof(patchDocument));
            id.ThrowIfNull(nameof(id));
            version.ThrowIfNull(nameof(version));
            
            var formsPdf = await GetByIdAsync(id, version);
            patchDocument.ApplyTo(formsPdf);

            formsPdf.ThrowIfNull(nameof(formsPdf));
            await _repository.SaveAsync(formsPdf.ToData());
        }

        public async Task DeleteAsync(Guid id, int version)
        {
            id.ThrowIfNull(nameof(id));
            version.ThrowIfNull(nameof(version));
            
            var formsPdf = await GetByIdAsync(id, version);

            formsPdf.ThrowIfNull(nameof(formsPdf));
            
            await _repository.DeleteAsync(id.ToString(), version.ToString());
        }
    }
}