using MyNotes.Entities;
using MyNotes.Data;

namespace MyNotes.Services;

public class UserServices{

  public DataContext _context;

  public UserServices(DataContext context)
    => _context = context;

  public async Task<bool> verify(string id)
    => await _context.Users.AnyAsync(x=>x.Id ==id);

  public async Task<User>get(string id)
    => await _context.Users.FirstOrDefaultAsync(x=>x.Id == id);

  public async Task<User>login(MyNotes.Endpoints.Auth.Login.Request req)
    => await _context.Users.FirstOrDefaultAsync(x=>x.email == req.email && x.password == req.password);
 
  public async Task create(User user){
    _context.Users.Add(user);
    await _context.SaveChangesAsync();
  }
  
  public async Task update(User user){
    _context.Users.Update(user);
    await _context.SaveChangesAsync();
  }

  public async Task remove(User user){
    _context.Users.Remove(user);
    await _context.SaveChangesAsync();
  }
}
