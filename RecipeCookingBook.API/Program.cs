using Microsoft.EntityFrameworkCore;
using RecipeCookingBook.API.Data;
using RecipeCookingBook.API.Extensions.ServiceExtensions;
using RecipeCookingBook.API.Repositories;
using RecipeCookingBook.API.Repositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureCors();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RecipeCookBookDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("RecipeBookDB"))
);
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<ILeftoverRepository, LeftoverRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("CorsPolicy");
}

app.UseCors("CorsPolicy");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();