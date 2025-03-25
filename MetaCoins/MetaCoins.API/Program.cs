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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MetaCoinsDbContext>(options =>{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(MetaCoinsDbContext)));
});

builder.Services.AddIdentity<UserEntity, RoleEntity>()
    .AddEntityFrameworkStores<MetaCoinsDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            });
    });

builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection(nameof(JwtConfiguration)));
var jwtConfig = builder.Configuration.GetSection(nameof(JwtConfiguration)).Get<JwtConfiguration>();

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;})
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>{
        options.TokenValidationParameters = new ()
        {
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey))
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

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IWalletsRepository, WalletsRepository>();
builder.Services.AddScoped<ITransactionsRepository, TransactionsRepository>();
builder.Services.AddScoped<IWalletsService, WalletsService>();
builder.Services.AddScoped<ITransactionsService, TransactionsService>();
builder.Services.AddScoped<ICoinsRepository, CoinsRepository>();
builder.Services.AddScoped<ICoinsService, CoinsService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IProfilesService, ProfilesService>();
builder.Services.AddScoped<IProfilesRepository, ProfilesRepository>();
builder.Services.AddScoped<ILikesRepository, LikesRepository>();
builder.Services.AddScoped<ILikesService, LikesService>();
builder.Services.AddScoped<IVotesRepository, VotesRepository>();
builder.Services.AddScoped<IVotesService, VotesService>();
builder.Services.AddHttpClient<IImageService, ImageService>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddAutoMapper(typeof(WalletProfile));
builder.Services.AddAutoMapper(typeof(TransactionProfile));
builder.Services.AddAutoMapper(typeof(CoinProfile));
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
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();
app.Run();