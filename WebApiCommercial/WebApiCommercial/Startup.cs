using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.Registrations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProfControl.WebApi;
using Repository;
using Service;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Text;

namespace WebAppCommercial
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
      services.AddControllers().AddNewtonsoftJson(options =>
      {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
      });
      services.AddDbContext<ContextBase>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

         services.Configure<GzipCompressionProviderOptions>(options =>
         {
            options.Level = CompressionLevel.Optimal;
         });

         services.AddResponseCompression(options =>
         {
            IEnumerable<string> MimeTypes = new[]
         {
         // General
         "text/plain",
         "text/html",
         "text/css",
         "font/woff2",
         "application/javascript",
         "image/x-icon",
         "image/png"
           };

            options.EnableForHttps = true;
            options.MimeTypes = MimeTypes;
            options.Providers.Add<GzipCompressionProvider>();
         });

         #region Registions
         //Usuário
         services.AddTransient<IUserService, UserService>();
         services.AddTransient<IGenericRepository<User>, UserRepository>();
         services.AddTransient<IUserRepository, UserRepository>();
         services.AddTransient<IBaseService<User>, UserService>();
         //Client
         services.AddTransient<IClientService, ClientService>();
         services.AddTransient<IGenericRepository<Client>, ClientRepository>();
         services.AddTransient<IClientRepository, ClientRepository>();
         services.AddTransient<IBaseService<Client>, ClientService>();
         //File
         services.AddTransient<IFileService, FileService>();
         services.AddTransient<IGenericRepository<File>, FileRepository>();
         services.AddTransient<IFileRepository, FileRepository>();
         services.AddTransient<IBaseService<File>, FileService>();
         //company
         services.AddTransient<ICompanyService, CompanyService>();
         services.AddTransient<IGenericRepository<Company>, CompanyRepository>();
         services.AddTransient<ICompanyRepository, CompanyRepository>();
         services.AddTransient<IBaseService<Company>, CompanyService>();
         //DescriptionFiles
         services.AddTransient<IDescriptionFilesService, DescriptionFilesService>();
         services.AddTransient<IGenericRepository<DescriptionFiles>, DescriptionFilesRepository>();
         services.AddTransient<IDescriptionFilesRepository, DescriptionFilesRepository>();
         services.AddTransient<IBaseService<DescriptionFiles>, DescriptionFilesService>();
         #endregion


         services.AddCors(options =>
      {
         options.AddPolicy("EnableCORS", builder =>
         {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build();
         });
      });

         services.AddCors();
         services.AddControllers();

         var key = Encoding.ASCII.GetBytes(Settings.Secret);
         services.AddAuthentication(x =>
         {
           x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
           x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
         })
         .AddJwtBearer(x =>
         {
           x.RequireHttpsMetadata = false;
           x.SaveToken = true;
           x.TokenValidationParameters = new TokenValidationParameters
           {
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(key),
             ValidateIssuer = false,
             ValidateAudience = false
           };
         }
         );

      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         var cultureInfo = new CultureInfo("pt-BR");
         //cultureInfo.NumberFormat.CurrencySymbol = "€";

         CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
         CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         app.UseMiddleware<ExceptionMiddleware>();

         app.UseHttpsRedirection();

         app.UseRouting();

         app.UseAuthentication();
         app.UseAuthorization();
         app.UseCors("EnableCORS");

         // Enable compression
         app.UseResponseCompression();
         app.UseStaticFiles();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
         UpdateDatabase(app);

       
      }

      private static void UpdateDatabase(IApplicationBuilder app)
      {
         using (var serviceScope = app.ApplicationServices
             .GetRequiredService<IServiceScopeFactory>()
             .CreateScope())
         {
            using (var context = serviceScope.ServiceProvider.GetService<ContextBase>())
            {
               context.Database.Migrate();
            }
         }
      }

   }
}
