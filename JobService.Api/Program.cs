var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddTransient<DataSeeder>();
builder.Services.AddAutoMapper(typeof(JobProfile),typeof(EmployerProfile),typeof(ApplicantProfile), typeof(ResumeProfile));
builder.Services.AddScoped <IEmployerService, EmployerService>(); 
builder.Services.AddScoped<IResumeService, ResumeService>();
builder.Services.AddScoped<IJobService, JobService.Infrastructure.Services.JobService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IApplicantService, ApplicantService>();
// builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:63343")
            .AllowAnyMethod()
            .AllowAnyHeader());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString
        ("DefaultConnection"));
});

var app = builder.Build();

const string filePathUploads = "C:\\bguir/JobService\\JobService.Core\\file";
if (!Directory.Exists(filePathUploads))
    Directory.CreateDirectory(filePathUploads);

var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

using (var scope = scopedFactory.CreateScope())
{
    var service = scope.ServiceProvider.GetService<DataSeeder>();
    service.SeedDataContext();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();