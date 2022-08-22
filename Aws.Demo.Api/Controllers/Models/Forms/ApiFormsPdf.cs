using System;

namespace Aws.Demo.Api.Model.Forms
{
    public class ApiFormsPdf
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public Guid FileId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public DateTime CreatedDate { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        Waiting = 1,
        Created,
        Published
    }
}