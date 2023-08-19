using MyNotes.Entities;

namespace MyNotes.Endpoints.Notes.Create;

public class Endpoint: Endpoint<Request>{
  
  private UserServices _userServices;
  private NotesServices _notesServices;

  public Endpoint(UserServices userServices, NotesServices notesServices){
    _userServices  = userServices; 
    _notesServices = notesServices;
  }
  
  public override void Configure(){
    Verbs(Http.POST);
    Post("api/user/{id}/note");
    AllowAnonymous();
  } 

  public override async Task<IResult> HandleAsync(Request req, CancellationToken ct){

    bool check = await _userServices.verify(Route<string>("id"));

    if(!check) return Results.NotFound("Usuario não encontrado!");

    var note = new Note(){
      title = req.title
    };

    await _notesServices.create(note);

    return Results.Ok(note);    
  }
}
