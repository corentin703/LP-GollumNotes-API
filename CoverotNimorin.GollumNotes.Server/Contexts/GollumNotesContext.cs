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
            
        modelBuilder.Entity<Note>()
            .HasMany<Picture>(note => note.Pictures)
            .WithOne(picture => picture.Note)
            .HasForeignKey(picture => picture.NoteId);
        
        modelBuilder.Entity<Note>()
            .HasOne<User>(note => note.User)
            .WithMany(user => user.Notes)
            .HasForeignKey(note => note.UserId);
        
        modelBuilder.Entity<Picture>().ToTable("Pictures");
        modelBuilder.Entity<User>().ToTable("Users");
    }
}