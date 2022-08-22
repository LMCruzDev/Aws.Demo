using System;
using Amazon;
using Amazon.CloudWatchLogs;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Amazon.SQS;
using Aws.Demo.Api.Business;
using Aws.Demo.Api.Business.Abstractions;
using Aws.Demo.Api.Configuration;
using Aws.Demo.Api.Data;
using Aws.Demo.Api.Data.Abstraction;
using Aws.Demo.Api.Data.Model;
using Aws.Demo.Api.Messaging;
using Aws.Demo.Api.Messaging.Abstraction;
using Aws.Demo.Api.Messaging.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Aws.Demo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var config = new AwsConfiguration();
            Configuration.Bind("AwsConfiguration", config);
            services.AddSingleton(config);

            services.AddSingleton(a => 
                new AmazonDynamoDBClient(
                    new SessionAWSCredentials(config.AccessKey, config.SecretKey, config.SecretToken),
                    RegionEndpoint.EUWest2));

            services.AddSingleton(a => 
                new AmazonSQSClient(
                    new SessionAWSCredentials(config.AccessKey, config.SecretKey, config.SecretToken),
                    RegionEndpoint.EUWest2));

            services.AddSingleton(a =>
                new AmazonCloudWatchLogsClient(
                    new SessionAWSCredentials(config.AccessKey, config.SecretKey, config.SecretToken),
                    RegionEndpoint.EUWest2));

            // Repositories
            services.AddSingleton<IRepository<DataFormsPdf, string, string>, PdfsRepository>();
            //services.AddSingleton<IRepository<DataProduct, Guid, Guid>, ProductsRepository>();
            
            // Services
            services.AddScoped<IPdfService, PdfService>();
            //services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAmazonS3Service, AmazonS3Service>();
            services.AddScoped<IFormConsentDocumentService, FormConsentDocumentService>();

            // Messaging
            services.AddScoped<IPublisher<FormPdfMessage>, Publisher>();

            services
                .AddControllers()
                .AddNewtonsoftJson();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Aws.Demo.Api", Version = "v1"});
            }).AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aws.Demo.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}