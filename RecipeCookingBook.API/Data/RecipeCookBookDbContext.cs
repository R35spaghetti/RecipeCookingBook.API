using Microsoft.EntityFrameworkCore;
using RecipeCookingBook.API.Models;

namespace RecipeCookingBook.API.Data;

public sealed class RecipeCookBookDbContext : DbContext
{
    public RecipeCookBookDbContext(DbContextOptions<RecipeCookBookDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public DbSet<Leftover> LeftOvers => Set<Leftover>();
}