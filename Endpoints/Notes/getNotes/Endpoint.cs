using MyNotes.Entities;

namespace MyNotes.Endpoints.Notes.GetNotes;

public class GetNotesEndpoint: EndpointWithoutRequest{

  private UserServices _userServices;
  private NotesServices _notesServices;

  public GetNotesEndpoint(UserServices userServices, NotesServices notesServices){
    _userServices  = userServices; 
    _notesServices = notesServices;
  }
   public override void Configure(){
    Get("/api/user/{userId}/notes");
    AllowAnonymous();
    Description(x=>x.WithTags("notes"));
  }

  public override async Task<IResult> HandleAsync(CancellationToken ct){
   
    bool check = await _userServices.verify(Route<string>("userId"));
   
    if(!check) return Results.NotFound("Usuario não encontrado!");

    var notes = await _notesServices.getNotes(Route<string>("userId"));

    return (notes is not null) 
      ? Results.Ok(notes.Select(x=>Mapper.toResonse(x)))
      : Results.NotFound();
  }
}
 
