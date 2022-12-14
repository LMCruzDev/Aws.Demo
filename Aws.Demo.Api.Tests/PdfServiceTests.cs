using AutoFixture;
using Aws.Demo.Api.Business;
using Aws.Demo.Api.Data.Abstraction;
using Aws.Demo.Api.Data.Model;
using Aws.Demo.Api.Messaging.Abstraction;
using Aws.Demo.Api.Messaging.Model;
using FluentAssertions;
using Moq;

namespace Aws.Demo.Api.Tests
{
    public class PdfServiceTests
    {
        private Mock<IRepository<DataFormsPdf, string, string>> repositoryMock;
        private Mock<IPublisher<FormPdfMessage>> publishedMock;
        private PdfService pdfService;
        private readonly Fixture fixture;

        public PdfServiceTests()
        {
            repositoryMock = new Mock<IRepository<DataFormsPdf, string, string>>();
            publishedMock = new Mock<IPublisher<FormPdfMessage>>();
            pdfService = new PdfService(repositoryMock.Object, publishedMock.Object);
            fixture = new Fixture();
        }

        [Fact]
        public async void ListAsync_WithValidId_ReturnsPdf()
        {
            // Arrange
            var totalItems = 10;
            var list = fixture
                .Build<DataFormsPdf>()
                .With(x => x.HashKey, Guid.NewGuid().ToString())
                .With(x => x.RangeKey, "1")
                .CreateMany(totalItems)
                .ToList();

            var pdfGuid = Guid.NewGuid();
            repositoryMock
                .Setup(x => x.ListAsync(pdfGuid.ToString()))
                .ReturnsAsync(list);

            // Act
            var result = await pdfService.ListAsync(pdfGuid);

            // Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(totalItems);
        }
    }
}