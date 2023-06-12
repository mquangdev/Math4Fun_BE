using Math4FunBackedn.DBContext;
using Math4FunBackedn.Repositories.AccountRepo;
using Math4FunBackedn.Repositories.AnswerRepo;
using Math4FunBackedn.Repositories.ChapterRepo;
using Math4FunBackedn.Repositories.CourseRepo;
using Math4FunBackedn.Repositories.LessonRepo;
using Math4FunBackedn.Repositories.QuestionRepo;
using Math4FunBackedn.Repositories.UserRepo;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

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
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.Run();
