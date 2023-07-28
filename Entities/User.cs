using System.ComponentModel.DataAnnotations;

namespace MyNotes.Entities;

public class User{

  [Key]
  public string Id { get; set; } = Guid.NewGuid().ToString();

  public string name { get; set; }
  
  public string email { get; set; }
  
  public string password { get; set; }

  public DateTime createdAt { get; set; } = DateTime.Now;
}
