using System.Text;
using System.Threading.RateLimiting;
using DOL.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WatchDog;
using WatchDog.src.Enums;
using Microsoft.AspNetCore.Hosting;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;


IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

var connectionString = configuration.GetConnectionString("ConnectionStr");


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();


#region Logging Watchdog Zone

//builder.Logging.AddWatchDogLogger();

//builder.Services.AddWatchDogServices();
//builder.Services.AddWatchDogServices(opt =>
//{
//    opt.IsAutoClear = true;
//    opt.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Weekly;

//    opt.SetExternalDbConnString = connectionString;
//    opt.DbDriverOption = WatchDogDbDriverEnum.PostgreSql;
//});

#endregion


builder.Services.AddDbContext<DolContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {

            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            //ValidIssuer = configuration["Jwt:Issuer"],
            //ValidAudience = configuration["Jwt:Audience"]
        };
    });


// Add Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DOL | API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter the JWT token in the field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 500,
                QueueLimit = 100,
                Window = TimeSpan.FromSeconds(1)
            }));

    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = 429;

        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
        {
            await context.HttpContext.Response.WriteAsync(
                $"Too many requests. Please try again after {retryAfter.TotalMinutes} minute(s). " +
                $"Read more about our rate limits at.", cancellationToken: token);
        }
        else
        {
            await context.HttpContext.Response.WriteAsync(
                "Too many requests. Please try again later. " +
                "Read more about our rate limits at.", cancellationToken: token);
        }
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("EnableCORS", builder =>
//    {
//        builder.WithOrigins().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true) // allow any origin
//       .AllowCredentials().Build();
//    });
//});

builder.Services.AddDbContext<DolContext>(options => options.UseNpgsql(connectionString));





var app = builder.Build();

app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(builder => builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.UseRateLimiter();

//app.UseWatchDog(opt =>
//{
//    opt.WatchPageUsername = configuration["WatchDogLogging:username"];
//    opt.WatchPagePassword = configuration["WatchDogLogging:password"];
//});

app.UseCors("AllowAll");



app.Run();

