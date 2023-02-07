using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RandomProject.Data;
using RandomProject.Repositories;
using RandomProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DATABASE CONFIGURATION

builder.Services.AddDbContext<DatabaseContext>(
                opt => opt.
                        UseMySQL(
                            builder.Configuration.GetConnectionString("DatabaseConnection")
                        )
                );

// REPOSITORY DEPENDENCIES

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();



// SERVICE DEPENDENCIES

builder.Services.AddScoped<IBookService, BookManager>();
builder.Services.AddScoped<IAuthorService, AuthorManager>();
builder.Services.AddScoped<IPublisherService, PublisherManager>();

// MODELSTATE VALID CONTROL

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = false;
});

// AUTOMAPPER
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
