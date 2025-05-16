using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using MetaCoins.DAL.Data;
using MetaCoins.Core.Entities.Identity;
using MetaCoins.DAL.Helpers;
using MetaCoins.Core.Interfaces.Services;
using MetaCoins.Core.Interfaces.Repositories;
using MetaCoins.DAL.Data.Repositories;
using MetaCoins.BLL.Services;
using MetaCoins.Core.Interfaces.Auth;
using MetaCoins.API.Helpers;
using Quartz;
using Amazon.S3;
using Microsoft.Extensions.Options;
using Amazon;
using Amazon.Runtime;

var builder = WebApplication.CreateBuilder(args);

// Database config
builder.Services.AddDbContext<MetaCoinsDbContext>(options =>{
    options.UseNpgsql(builder.Configuration.GetConnectionString("MetaCoinsDbContext"));
});


builder.Services.AddIdentity<UserEntity, RoleEntity>()
    .AddEntityFrameworkStores<MetaCoinsDbContext>()
    .AddDefaultTokenProviders();

// S3 Settings Configuration
builder.Services.Configure<S3Settings>(builder.Configuration.GetSection(nameof(S3Settings)));

builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var s3settings = sp.GetRequiredService<IOptions<S3Settings>>().Value;
    var credentials = new BasicAWSCredentials(s3settings.AccessKey, s3settings.SecretKey);
    var config = new AmazonS3Config
    {
        RegionEndpoint = RegionEndpoint.GetBySystemName(s3settings.Region)
    };
    return new AmazonS3Client(credentials, config);
});

// Quartz config
builder.Services.AddQuartz(q =>{
    q.UseMicrosoftDependencyInjectionJobFactory();

    QuartzJobScheduler.ConfigureQuartzJobs(q);
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
builder.Services.AddScoped(provider =>
{
    var schedulerFactory = provider.GetRequiredService<ISchedulerFactory>();
    return schedulerFactory.GetScheduler().GetAwaiter().GetResult();
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder =>
            {
                builder.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            });
    });

// Jwt configuration
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection(nameof(JwtConfiguration)));
var jwtConfig = builder.Configuration.GetSection(nameof(JwtConfiguration)).Get<JwtConfiguration>();
builder.Services.AddSingleton<IJwtProvider, JwtProvider>();

if (string.IsNullOrEmpty(jwtConfig.SecretKey))
{
    throw new Exception("SecretKey is null or empty");
}

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;})
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>{
        options.TokenValidationParameters = new ()
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JwtConfiguration:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JwtConfiguration:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey)),
            ValidateIssuerSigningKey = true
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Cookies["JwtToken"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CustomerPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Customer");
        policy.RequireClaim("userId");
    });
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Admin");
    });
    options.AddPolicy("AdminOrCustomerPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Admin", "Customer");
    });
});

// Services Scope
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IWalletsService, WalletsService>();
builder.Services.AddScoped<ICoinTransactionsService, CoinTransactionsService>();
builder.Services.AddScoped<ICoinPurchaseRequestsService, CoinPurchaseRequestsService>();
builder.Services.AddScoped<ICoinSellOrdersService, CoinSellOrdersService>();
builder.Services.AddScoped<ICoinsService, CoinsService>();
builder.Services.AddScoped<IProfilesService, ProfilesService>();
builder.Services.AddScoped<ILikesService, LikesService>();
builder.Services.AddScoped<ICoinVotesService, CoinVotesService>();
builder.Services.AddScoped<IVotingSessionsService, VotingSessionsService>();

// Repositories Scope
builder.Services.AddScoped<IWalletsRepository, WalletsRepository>();
builder.Services.AddScoped<ICoinTransactionsRepository, CoinTransactionsRepository>();
builder.Services.AddScoped<ICoinSellOrdersRepository, CoinSellOrdersRepository>();
builder.Services.AddScoped<ICoinPurchaseRequestsRepository, CoinPurchaseRequestsRepository>();
builder.Services.AddScoped<ICoinsRepository, CoinsRepository>();
builder.Services.AddScoped<IProfilesRepository, ProfilesRepository>();
builder.Services.AddScoped<ILikesRepository, LikesRepository>();
builder.Services.AddScoped<ICoinVotesRepository, CoinVotesRepository>();
builder.Services.AddScoped<IVotingSessionsRepository, VotingSessionsRepository>();

// Email service
builder.Services.AddTransient<IEmailService, EmailService>();

// HttpClient Configuration
builder.Services.AddHttpClient<IImageService, ImageService>();

// JWT Configuration
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection(nameof(JwtConfiguration)));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    // Add Bearer token security definition
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and your token in the text input below.\nExample: \"Bearer abc123xyz\""
    });

    // Add global security requirement
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();
app.Run();