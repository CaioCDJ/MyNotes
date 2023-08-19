using MyNotes.Entities;

namespace MyNotes.Endpoints.Notes.Delete;

public class Endpoint: Endpoint<Request>{
  
  private UserServices _userServices;
  private NotesServices _notesServices;

  public Endpoint(UserServices userServices, NotesServices notesServices){
    _userServices  = userServices; 
    _notesServices = notesServices;
  }
  
  public override void Configure(){
    Verbs(Http.DELETE);
    Delete("api/user/{userId}/note/{noteId}");
    AllowAnonymous();
  } 

  public override async Task HandleAsync(Request req, CancellationToken ct){

    bool check = await _userServices.verify(req.userId);

    if(!check) await SendNotFoundAsync(ct);

    var note = await _notesServices.getNote(req.noteId,req.userId);

    if(note is null) await SendNotFoundAsync(ct);

    else{
      await _notesServices.remove(note);
      await SendOkAsync(ct);
    }
  }
}

