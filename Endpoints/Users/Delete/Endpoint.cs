using MyNotes.Entities;

namespace MyNotes.Endpoints.Users.Delete;

public class DeleteUserEndpoint: EndpointWithoutRequest{

  private DataContext _datacontext;

  public DeleteUserEndpoint(DataContext dataContext)
    => _datacontext = dataContext;

  public override void Configure(){
    Delete("/api/user/{id}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken ct){
    
    string id = Route<string>("id");
    
    if(id is null || string.IsNullOrEmpty(id))  await SendErrorsAsync();
   
    var user = await _datacontext.Users.FirstOrDefaultAsync(x=>x.Id == id);

    if(user is not null){

      _datacontext.Users.Remove(user);
      await _datacontext.SaveChangesAsync();

      await SendOkAsync(ct);
    }
    else
      await SendNotFoundAsync(ct);


  }
}
 
