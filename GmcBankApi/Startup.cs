
using GmcBank;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace GmcBankApi
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
            //string securityKey = "Hello achour";
            //var signingKey = new SymmetricSecurityKey(
            //   Encoding.UTF8.GetBytes(securityKey));


            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {

            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            // Clock skew compensates for server time drift.
            //            // We recommend 5 minutes or less:
            //            ClockSkew = TimeSpan.FromMinutes(5),
            //            // Specify the key used to sign the token:
            //            IssuerSigningKey = signingKey,

            //            RequireSignedTokens = true,
            //            // Ensure the token hasn't expired:
            //            RequireExpirationTime = true,
            //            ValidateLifetime = true,
            //            // Ensure the token audience matches our audience value (default true):
            //            ValidateAudience = true,
            //            ValidAudience = Configuration["Auth:Audience"],
            //            // Ensure the token was issued by a trusted authorization server (default true):
            //            ValidateIssuer = true,
            //            ValidIssuer = Configuration["Auth:Issuer"]
            //        };
            //    });

            // injecting bank dependency 
            services
             .AddScoped<IBank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> , Bank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction>>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "gmc bank API",
                    Description = "dunno \\_",
                    TermsOfService = "",
                    Contact = new Contact
                    {
                        Name = "arsslen",
                        Email = string.Empty,
                        Url = "http://achour.101"
                    },
                    License = new License
                    {
                        Name = "mine",
                        Url = "https://mochtawa.wow"
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);


            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();


            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseAuthentication();


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dhabout Lguerda API V1");
            });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        /*{
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }*/
    }
}
