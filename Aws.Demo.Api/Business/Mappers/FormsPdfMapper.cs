using System;
using System.Collections.Generic;
using System.Linq;
using Aws.Demo.Api.Data.Model;
using Aws.Demo.Api.Messaging.Model;
using Aws.Demo.Api.Model.Forms;

namespace Aws.Demo.Api.Business.Mappers
{
    public static class FormsPdfMapper
    {
        public static ApiFormsPdf ToModel(this DataFormsPdf model)
        {
            if (model == null)
            {
                return null;
            }

            return new()
            {
                Id = Guid.Parse(model.HashKey),
                Version = int.Parse(model.RangeKey),
                FileId = model.FileId,
                Name = model.Name,
                CreatedDate = model.CreatedDate,
                Status = (Status)model.Status
            };
        }

        public static ApiFormsPdf ToModel(this ApiAddFormsPdf model)
        {
            return new()
            {
                Id = model.Id,
                Version = 1,
                FileId = Guid.NewGuid(),
                Name = model.Name,
                CreatedDate = DateTime.UtcNow,
                Status = Status.Waiting
            };
        }

        public static List<ApiFormsPdf> ToModel(this List<DataFormsPdf> list)
        {
            return list.Select(ToModel)?.OrderByDescending(l => l.Version).ToList();
        }

        public static DataFormsPdf ToData(this ApiFormsPdf model)
        {
            return new()
            {
                RangeKey = model.Version.ToString(),
                HashKey = model.Id.ToString(),
                FileId = model.FileId,
                Name = model.Name,
                CreatedDate = model.CreatedDate,
                Status = (int)model.Status
            };
        }

        public static FormPdfMessage ToMessage(this ApiAddFormsPdf model)
        {
            return new()
            {
                Guid = Guid.NewGuid(),
                Name = model.Name,
                Html = model.Html
            };
        }
    }
}