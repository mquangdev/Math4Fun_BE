using Math4FunBackedn.DBContext;
using Math4FunBackedn.Identity;
using Math4FunBackedn.Repositories.AccountRepo;
using Math4FunBackedn.Repositories.AnswerRepo;
using Math4FunBackedn.Repositories.ChapterRepo;
using Math4FunBackedn.Repositories.CourseRepo;
using Math4FunBackedn.Repositories.LessonRepo;
using Math4FunBackedn.Repositories.MailRepo;
using Math4FunBackedn.Repositories.QuestionRepo;
using Math4FunBackedn.Repositories.TokenRepo;
using Math4FunBackedn.Repositories.UserRepo;
using Math4FunBackedn.Settings;
using Math4FunBackedn.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var Configuration = new ConfigurationBuilder();
var config = builder.Configuration;
// Bearer authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization(options  =>
{
    options.AddPolicy(IdentityData.AdminUserPolicyName, p => p.RequireClaim(IdentityData.AdminUserClaimName, "true"));
});
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Add Config for Required Email


// Repository
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped(typeof(IAccountRepository), typeof(AccountRepository));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped(typeof(ICourseRepository), typeof(CourseRepository));
builder.Services.AddScoped(typeof(IChapterRepository), typeof(ChapterRepository));
builder.Services.AddScoped(typeof(ILessonRepository), typeof(LessonRepository));
builder.Services.AddScoped(typeof(IQuestionRepository), typeof(QuestionRepository));
builder.Services.AddScoped(typeof(IAnswerRepository), typeof(AnswerRepository));
builder.Services.AddScoped(typeof(IMailRepository), typeof(MailRepository));
builder.Services.AddScoped(typeof(ITokenRepository), typeof(TokenRepository));
builder.Services.AddScoped(typeof(IEmailSender), typeof(MailKitEmailSender));

builder.Services.AddMailModule(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCors", build =>
    {
        build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyCors");
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.Run();
