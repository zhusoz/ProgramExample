using Autofac;
using Autofac.Core;
using DataSharing.Common;
using DataSharing.Filter;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using ProblemDisposal.Common.Basic;
using ProgramsNetCore.Common;
using ProgramsNetCore.Common.Autofac;
using ProgramsNetCore.Common.AutoMapper;
using ProgramsNetCore.Common.Extensions;
using ProgramsNetCore.Common.GlobalRouting;
using ProgramsNetCore.Common.Hubs;
using ProgramsNetCore.Common.JWT;
using ProgramsNetCore.Common.Quartz;
using ProgramsNetCore.Middlewares;
using ProgramsNetCore.PolicyRequirement;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static ProgramsNetCore.Common.PublicEnum;

namespace ProgramsNetCore
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
            services.Configure<ApiBehaviorOptions>(option =>
            {
                option.SuppressModelStateInvalidFilter = true;
            });


        

            services.AddHttpContextAccessor();
            services.AddSingleton<Models.ILogger, Logger>();

            services.AddControllers(op => {
                ///ȫ��·��
                op.UseCentralRoutePrefix(new RouteAttribute("api/[Controller]/[Action]"));
                op.Filters.Add(new ApiValidationFilter());
            })
 .AddNewtonsoftJson(op =>
 { //Newtonsoft.Json  
        op.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm";
        //���л��ͷ����л�ʱ,����Ĭ��ֵ
        op.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        // ����ѭ������,���EntityFramework��������json���л�ʱ��ѭ����������
        op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

     op.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

 });

            services.AddSingleton(new Appsettings(Configuration));
            services.AddSingleton<IAuthorizationHandler, MustRoleHandler>();
            services.Configure<FormOptions>(options =>
            {
                // Set the limit to 256 MB
                options.MultipartBodyLengthLimit = 268435456;
            }).Configure<KestrelServerOptions>(option => {
                Configuration.GetSection("Kestrel");
                option.AllowSynchronousIO = true;
            }).Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "An ASP.NET Core Web API for managing ToDo items",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Wei Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });

                #region Token�󶨵�ConfigureServices
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "����Bearer token",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference =new OpenApiReference
                            {
                                Type =ReferenceType.SecurityScheme ,Id ="Bearer"
                            }
                        },new string[]{ }
                    }
                });


                #endregion

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename),true);
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"ProgramsNetCore.Models.xml"), true);
            });

            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });
            services.AddAuthorization(options =>
            {
                //������Ȩ�����
                //���ڽ�ɫ 
                options.AddPolicy("S_AdminPolicy", o => {
                    //o.RequireClaim(ClaimTypes.Role, Common.PublicEnum.AuthorizeType.S_Admin.ToString()).Build();//CliamType=Type    
                    o.RequireRole(((int)Common.PublicEnum.AuthorizeType.S_Admin).ToString());//CliamType=Type    
                });
                options.AddPolicy("S_SecurityPolicy", o => {                   
                    o.RequireRole(((int)Common.PublicEnum.AuthorizeType.S_Security).ToString());//CliamType=Type    
                });
                options.AddPolicy("AdminPolicy", o => {                  
                    o.RequireRole(((int)Common.PublicEnum.AuthorizeType.Admin).ToString(),((int)Common.PublicEnum.AuthorizeType.G_Admin).ToString());//CliamType=Type    
                });
                options.AddPolicy("S_OperatorPolicy", o => {             
                    o.RequireRole(((int)Common.PublicEnum.AuthorizeType.S_Operator).ToString());//CliamType=Type    
                });
                options.AddPolicy("S_AuditorPolicy", o => {
                    o.RequireRole(((int)Common.PublicEnum.AuthorizeType.S_Auditor).ToString());//CliamType=Type    
                });
                ////��������  ����ָ��������
                //options.AddPolicy("ClaimPolicy", o => {
                //    o.RequireClaim("","");//ClaimType   
                //});
                ////�������� 
                //options.AddPolicy("", o => {
                //    //�̳��� IAuthorizationRequirement �Զ���
                //    o.Requirements.Add(new AdminRequirement() {  });
                //});

            }).AddAuthentication(options =>
            {
                //options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer",async options => {

                //options.Audience = "Audience";
                //options.TokenValidationParameters = new TokenValidationParameters
                //{
                //    ValidateIssuer = true,//�Ƿ���֤Issuer
                //    ValidateAudience = true,//�Ƿ���֤Audience
                //    ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                //    ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                //    ValidIssuer = Configuration["Audience:Issuer"],//appsettings.json�ļ��ж����Issuer 
                //    ValidAudience = Configuration["Audience:Audience"],//appsettings.json�ļ��ж����Audience
                //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Audience:Secret"]))
                //};
                //options.SaveToken = true;
                //options.RequireHttpsMetadata = false;
                //IdentityModelEventSource.ShowPII = true;
                //options.Authority = "http://localhost:401/api/PushHub";
                options.Events = new JwtBearerEvents
                {
                   
                    //�˴�ΪȨ����֤ʧ�ܺ󴥷����¼�
                    OnChallenge = context =>
                    {
                        //�˴�����Ϊ��ֹ.Net CoreĬ�ϵķ������ͺ����ݽ�����������ҪŶ������
                        context.HandleResponse();
                        //�Զ����Լ���Ҫ���ص����ݽ����������Ҫ���ص���Json����ͨ������Newtonsoft.Json�����ת��
                        var payload = new AjaxResult<string> {  StatusCode=403, Data="��֤ʧ�ܣ���Ȩ��" };
                        //�Զ��巵�ص���������
                        context.Response.ContentType = "application/json";
                        //�Զ��巵��״̬�룬Ĭ��Ϊ401 ������ĳ� 200
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        //���Json���ݽ��
                        context.Response.WriteAsync( Newtonsoft.Json.JsonConvert.SerializeObject(payload));
                        return Task.FromResult(0);
                    }
                    //,
                    //OnTokenValidated = async context =>
                    //{
                    //    var token = context.Request.Query["access_token"];
                    //    TokenModel tm = JwtHelper.SerializeJwt(token);

                    //    //��Ȩ
                    //    var claimList = new List<Claim>();
                    //    var claim = new Claim(ClaimTypes.Role, tm.Role.ToString());

                    //    //Test-By-ZSZ
                    //    claimList.Add(new Claim("Permission", tm.Permissions));
                    //    claimList.Add(new Claim(JwtRegisteredClaimNames.NameId, tm.UId.ToString()));
                    //    claimList.Add(new Claim(ClaimTypes.NameIdentifier, tm.UId.ToString()));

                    //    claimList.Add(claim);
                    //    var identity = new ClaimsIdentity(claimList);
                    //    var principal = new ClaimsPrincipal(identity);
                    //    context.HttpContext.User = principal;

                    //}
                };

            });

            services.AddSignalR();
            services.AddCors(option => option.AddPolicy("Domain",
         builder =>
         builder.AllowAnyMethod()
         .AllowAnyHeader()
         .SetIsOriginAllowed(_ => true)
         .AllowCredentials())
         );

            //AutoMapperӳ��
            services.AddAutoMapperSetup();
        }


        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModuleRegister());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            ServiceLocator.Instance = app.ApplicationServices;
            ServiceLocator.ApplicationBuilder = app;

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseStaticHttpContext();
            app.UseDbLoggerRecord();

            app.UseStaticFiles();

            app.UseJwtTokenAuth();

            //�����м����������Swagger��ΪJSON�ս��
            app.UseSwagger();
            //�����м�������swagger-ui��ָ��Swagger JSON�ս��
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseCors("Domain");
            //Ĭ������cookie
            app.UseCookiePolicy();
            //������Ȩ
            app.UseAuthorization();
            //������֤
            app.UseAuthentication();
            //���ش�����
            app.UseStatusCodePages();

            app.UseQuartz();


            //   app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders=ForwardedHeaders.XForwardedFor|ForwardedHeaders.XForwardedProto });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<PushHub>("/api/PushHub");
                endpoints.MapControllers();
            });
        }
    }


}