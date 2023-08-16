using MyNotes.Entities;

namespace MyNotes.Endpoints.Notes.GetNotes;

public class GetNotesEndpoint: EndpointWithoutRequest{

  private DataContext _datacontext;

  public GetNotesEndpoint(DataContext dataContext)
    => _datacontext = dataContext;

  public override void Configure(){
    Get("/api/user/{userId}/notes");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken ct){
    
    string id = Route<string>("userId");
    
    if(id is null || string.IsNullOrEmpty(id))  await SendErrorsAsync();
    
    var user = await _datacontext.Users.FirstOrDefaultAsync(x=>x.Id == id);

    if(user is null) await SendNotFoundAsync(ct);

    var notes = _datacontext.Notes
      .Where(x=>x.userId == user.Id)
      .Select(x=>Mapper.toResonse(x));
    
    SendOkAsync(notes);

  }
}
 
