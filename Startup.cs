using System;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using work_platform_backend.Authorization;
using work_platform_backend.Models;
using work_platform_backend.Repos;
using work_platform_backend.Services;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using work_platform_backend.Hubs;
using Serilog;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace work_platform_backend
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
            services.AddControllers()
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


             services.AddSwaggerGen(c =>
             {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }); 
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                                Enter 'Bearer' [space] and then your token in the text input below.
                                \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                
          

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                            },
                            new List<string>()
                        }
                        });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
             });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddScoped<AuthService>();
            services.AddScoped<ICheckpointRepository, CheckpointRepo>();
            services.AddScoped<CheckPointService>();
            services.AddScoped<ITeamRepository, TeamRepo>();
            services.AddScoped<TeamService>();
            services.AddScoped<IProjectRepository, ProjectRepo>();
            services.AddScoped<ProjectService>();
            services.AddScoped<IAttachmentRepository, AttachmentRepo>();
            services.AddScoped<ICommentRepository,CommentRepository>();
            services.AddScoped<AttachmentService>();
            services.AddScoped<IRoomRepository, RoomRepo>();
            services.AddScoped<RoomService>();
            services.AddScoped<ISettingRepository, SettingRepo>();
            services.AddScoped<SettingService>();
            services.AddScoped<IRTaskRepository, RTaskRepo>();
            services.AddScoped<TaskService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<UserService>();
            services.AddScoped<ProjectManagerService>();
            services.AddScoped<CommentService>();
            services.AddScoped<SessionService>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<ISessionRepository,SessionRepository>();
            services.AddScoped<ITeamMembersRepository,TeamMembersRepository>();
            


            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddScoped<IChatMessageTypeRepository, ChatMessageTypeRepository>();
            services.AddScoped<ITeamChatRepository, TeamChatRepository>();
            services.AddScoped<ChatMessageService>();
            services.AddScoped<ChatMessageService>();
            services.AddScoped<ChatMessageTypeService>();
            services.AddScoped<TeamChatService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<INotificationRepository,NotificationRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<ApplicationContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            },ServiceLifetime.Transient);         

            var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailService,EmailService>();

             services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 5;
                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Configuration["Jwt:Issuer"],
            ValidAudience = Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
            ClockSkew = TimeSpan.Zero
            };
            });



            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.LEADER,Policies.LeaderPolicy());
                config.AddPolicy(Policies.MEMBER,Policies.MemberPolicy());
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
        
                services.AddSignalR();               
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                              ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // loggerFactory.AddSerilog();
            
            //app.UseHttpsRedirection();

            app.UseRouting();

                  // Enable middleware to serve generated Swagger as a JSON endpoint.
          
    // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });      

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints((routes) =>
            { 
                routes.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

               routes.MapControllers();
               routes.MapHub<ChatHub>("/chathub");
               routes.MapHub<NotificationHub>("/notificationHub");
            });

         
            var MyAssembly = Assembly.GetExecutingAssembly()
                .GetName();

            // Log.Logger = new LoggerConfiguration()
            //     .ReadFrom.Configuration(Configuration)
            //     .MinimumLevel.Debug()
            //     .Enrich.FromLogContext()
            //     .Enrich.WithMachineName()
            //     .Enrich.WithProperty(nameof(MyAssembly.Name), $"{MyAssembly.Name}")
            //     .Enrich.WithProperty(nameof(MyAssembly.Version), $"{MyAssembly.Version}")
            //     .CreateLogger();


        }
        
    }
}
