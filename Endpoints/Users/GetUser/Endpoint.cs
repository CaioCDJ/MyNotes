using MyNotes.Entities;

namespace MyNotes.Endpoints.Users.GetUser;

public class GetUserEndpoint: EndpointWithoutRequest{

  private DataContext _datacontext;

  public GetUserEndpoint(DataContext dataContext)
    => _datacontext = dataContext;

  public override void Configure(){
    Get("/api/user/{id}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken ct){
    
    string id = Route<string>("id");
    
    if(id is null || string.IsNullOrEmpty(id))  await SendErrorsAsync();
    
    var user = await _datacontext.Users.FirstOrDefaultAsync(x=>x.Id == id);

    if(user is not null) await SendOkAsync(user,ct);

    else await SendNotFoundAsync(ct); 
  
  }
}
 
