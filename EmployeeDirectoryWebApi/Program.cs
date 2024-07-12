using EmployeeDirectoryWebApi.Data.Contract;
using EmployeeDirectoryWebApi.Data.Models;
using EmployeeDirectoryWebApi.Data.Repositary;
using EmployeeDirectoryWebApi.Services;
using EmployeeDirectoryWebApi.Services.Common;
using EmployeeDirectoryWebApi.Services.Contract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins( "http://localhost:4200")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddScoped<IEmployeeRepositary, EmployeeRepositary>();
builder.Services.AddScoped<IRoleRepositary, RoleRepositary>();
builder.Services.AddScoped<IManagerRepositary, ManagerRepositary>();
builder.Services.AddScoped(typeof(IGenericRepositary<>), typeof(GenericRepositary<>));
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<SagarEmployeeDirectoryDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
