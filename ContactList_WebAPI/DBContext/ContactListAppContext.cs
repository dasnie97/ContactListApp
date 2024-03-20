using ContactList_Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ContactList_WebAPI.DBContext;

public class ContactListAppContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }

    private IConfiguration _configuration;

    public ContactListAppContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ContactListApp_Database"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>()
            .ToTable("Contacts")
            .HasKey(c => c.Id);

        modelBuilder.Entity<Category>()
            .ToTable("Categories")
            .HasKey(c => c.Id);

        modelBuilder.Entity<Category>()
            .HasMany(a => a.Contacts)
            .WithOne(b => b.Category)
            .HasForeignKey(b => b.CategoryId)
            .IsRequired();
    }
}
