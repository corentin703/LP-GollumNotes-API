using CoverotNimorin.GollumNotes.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoverotNimorin.GollumNotes.Server.Contexts;

public class GollumNotesContext : DbContext
{
    public DbSet<Note> Notes { get; set; } = default!;
    public DbSet<Picture> Picture { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    
    public GollumNotesContext (DbContextOptions<GollumNotesContext> options)
        : base(options)
    {
        //
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>().ToTable("Notes");
        modelBuilder.Entity<Picture>().ToTable("Pictures");
        modelBuilder.Entity<User>().ToTable("Users");
    }
}