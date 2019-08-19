using Microsoft.AspNetCore.Builder;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Aliases.Extensions;
using StorageRepository;

namespace Aliases
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

            var dbPath = Configuration.GetSection("StorageRepo").GetSection("PathToDb").Value;

            ConfigureApiVersioning(services);
            ConfigureCors(services);
            ConfigureSwagger(services);            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IDefaultStorageRepository>(new DefaultStorageRepository(dbPath));
        }

        public void ConfigureSwagger(IServiceCollection services)
        {

            var contact = new Contact() { Name = "Whatisthejava", Email = "contact@contact.com", Url = "www.github.com" };

            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<ApiVersionOperationFilter>();

                c.CustomSchemaIds(i => i.AssemblyQualifiedName + "." + i.FullName);


                c.SwaggerDoc("v1.0", new Info { Version = "v1.0", Title = "V1", Description = "V1 Endpoints", TermsOfService = "None", Contact = contact });
                c.SwaggerDoc("v2.0", new Info { Version = "v2.0", Title = "V2", Description = "V2 Endpoints", TermsOfService = "None", Contact = contact });
                c.SwaggerDoc("v3.0", new Info { Version = "v3.0", Title = "V3", Description = "V3 Endpoints", TermsOfService = "None", Contact = contact });
                c.SwaggerDoc("v4.0", new Info { Version = "v4.0", Title = "V4", Description = "V4 Endpoints", TermsOfService = "None", Contact = contact });
                c.SwaggerDoc("v5.0", new Info { Version = "v5.0", Title = "V5", Description = "V5 Endpoints", TermsOfService = "None", Contact = contact });



                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var actionApiVersionModel = apiDesc.ActionDescriptor?.GetApiVersion();
                    if (actionApiVersionModel == null)
                    {
                        return true;
                    }
                    if (actionApiVersionModel.DeclaredApiVersions.Any())
                    {
                        return actionApiVersionModel.DeclaredApiVersions.Any(v => $"v{v.ToString()}" == docName);
                    }
                    return actionApiVersionModel.ImplementedApiVersions.Any(v => $"v{v.ToString()}" == docName);
                });
            });
        }

        public void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });
        }

        public void ConfigureApiVersioning(IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(5, 0); 
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;
                o.ApiVersionReader = new RouteApiReader();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            AddSwagger(app);

            app.UseCors("AllowAll");
            

            app.UseMvc();
        }

        public void AddSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v4.0/swagger.json", "V5");
                c.SwaggerEndpoint("/swagger/v4.0/swagger.json", "V4");
                c.SwaggerEndpoint("/swagger/v3.0/swagger.json", "V3");
                c.SwaggerEndpoint("/swagger/v2.0/swagger.json", "V2");
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "V1");
            });
        }
    }
}
