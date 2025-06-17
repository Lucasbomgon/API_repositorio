using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { 
    }

    public DbSet<Categoria>? categorias { get; set; }
    public object Categorias { get; internal set; }
    public DbSet<Produto>? produtos { get; set; }   
}
