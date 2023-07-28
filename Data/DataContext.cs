using Microsoft.EntityFrameworkCore;
using MyNotes.Entities;

namespace MyNotes.Data;

public class DataContext : DbContext{

  public DataContext(DbContextOptions<DataContext>options): base(options){}
 
  public DbSet<User> Users => Set<User>();
  public DbSet<Note> Notes => Set<Note>();

}
