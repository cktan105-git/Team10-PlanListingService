using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using PlantListing.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlantListing.Extensions;
using PlantListing.Images;
using PlantListing.Integrations;
using Amazon.XRay.Recorder.Handlers.AwsSdk;

namespace PlantListing
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSwagger() // Adds Swagger
                .AddCustomMVC() // AddControllers with custom filter
                .AddCustomDbContext(Configuration) // Adds PlantDetailsContext as EF context
                .AddDefaultAWSOptions(Configuration.GetAWSOptions()) // Adds AWS Options
                .AddCognitoIdentity() // Adds Amazon Cognito as Identity Provider
                .AddAWSService<IAmazonS3>() // Adds Amazon S3
                .Configure<AWSSettings>(Configuration.GetSection(AWSSettings.AWS)) // Read option from "AWS" environment variable
                .AddTransient<IPlantImageService, PlantImageService>()
                .Configure<ProducerServiceSettings>(Configuration.GetSection(ProducerServiceSettings.ProducerService)) // Read option from "AWS" environment variable
                .AddTransient<IProducerService, ProducerService>();
                
            services.AddHttpClient<ProducerServiceClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlantListing v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
                });
            });

            //var loggerOptions = new LambdaLoggerOptions(Configuration);

            //// Configure Lambda logging
            //loggerFactory.AddLambdaLogger(loggerOptions);
        }
    }
}
