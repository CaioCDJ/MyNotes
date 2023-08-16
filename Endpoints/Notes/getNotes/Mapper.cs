using MyNotes.Entities;

namespace MyNotes.Endpoints.Notes.GetNotes;

public class Mapper{

  public static Response toResonse(Note note){
    return new Response(
        note.Id, 
        note.title,
        note.createdAt,
        note.updatedAt
        );
  }
}
