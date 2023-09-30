using MyNotes.Entities;
using MyNotes.Data;

namespace MyNotes.Services;

public class NotesServices{

  public DataContext _context;

  public NotesServices(DataContext context)
    => _context = context;

  public async Task<Note>getNote(string id,string userId)
    => await _context.Notes.FirstOrDefaultAsync(x=>x.Id == id && x.userId == userId);

  public async Task<List<Note>>getNotes(string userId)
    => await _context.Notes.Where(x=>x.userId == userId).ToListAsync();


  public async Task create(Note note){
    _context.Notes.Add(note);
    await _context.SaveChangesAsync();
  }
 
  public async Task update(Note note){
    _context.Notes.Update(note);
    await _context.SaveChangesAsync();
  }

  public async Task remove(Note note){
    _context.Notes.Remove(note);
    await _context.SaveChangesAsync();
  }
  
}

