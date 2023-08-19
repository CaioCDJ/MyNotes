using MyNotes.Entities;

namespace MyNotes.Endpoints.Notes.Update;

public class UpdateNoteEndpoint: Endpoint<Request,Note>{

  private UserServices _userServices;
  private NotesServices _notesServices;

  public UpdateNoteEndpoint(UserServices userServices, NotesServices notesServices){
    _userServices  = userServices; 
    _notesServices = notesServices;
  }

  public override void Configure(){
    Verbs(Http.GET);
    Put("api/user/{userId}/note/{id}");
    Description(x=>x.WithTags("notes"));
  }

  public override async Task HandleAsync(Request req, CancellationToken ct){
    
    bool check = await _userServices.verify(Route<string>("userId"));

    if(!check) await SendNotFoundAsync(ct);

    var note = await _notesServices.getNote(Route<string>("id"),Route<string>("userId"));

    if(note is null) await SendNotFoundAsync(ct);

    note.title = req.title ?? note.title;
    note.text = req.text ?? note.text;

    note.updatedAt = DateTime.Now;

    await _notesServices.update(note);

    await SendOkAsync(note,ct);
  
  }
}
