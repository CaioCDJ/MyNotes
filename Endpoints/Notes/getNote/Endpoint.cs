using MyNotes.Entities;

namespace MyNotes.Endpoints.Notes.GetNote;

public class GetNotesEndpoint: Endpoint<Request,Note>{

  private UserServices _userServices;
  private NotesServices _notesServices;

  public GetNotesEndpoint(UserServices userServices, NotesServices notesServices){
    _userServices  = userServices; 
    _notesServices = notesServices;
  }

  public override void Configure(){
    Verbs(Http.GET);
    Get("api/user/{userId}/note/{id}");
    Description(x=>x.WithTags("notes"));
  }

  public override async Task<IResult> HandleAsync(Request req, CancellationToken ct){
  
    bool check = await _userServices.verify(req.userId);

    if(!check) return Results.NotFound("Usuario não encontrado!");

    var note = await _notesServices.getNote(req.id,req.userId);

    return (note is not null)
      ? Results.Ok(note)
      : Results.NotFound("Nota não encontrada!");
  }
}
