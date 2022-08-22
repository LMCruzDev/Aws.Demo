using System;
using Aws.Demo.Api.Messaging.Abstraction;

namespace Aws.Demo.Api.Messaging.Model
{
    public class FormPdfMessage : IMessage<Guid>
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string Html { get; set; }
    }
}
