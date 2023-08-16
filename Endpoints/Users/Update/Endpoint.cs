namespace MyNotes.Endpoints.Users.Update;

public class UpdateUserEndpoint: Endpoint<Request>{

  private DataContext _datacontext;

  public UpdateUserEndpoint(DataContext dataContext)
    => _datacontext = dataContext;

  public override void Configure(){
    Put("/api/user/{id}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(Request request,CancellationToken ct){
  
    string id = Route<string>("id");
    
    var user = await _datacontext.Users.FirstOrDefaultAsync(
      x=>x.Id == id);

    if(user is not null){
     
      user.name = (request.name is not null)
        ? request.name 
        : user.name;

      user.email = (request.email is not null)
        ? request.email 
        : user.email;

      _datacontext.Users.Update(user);
      await _datacontext.SaveChangesAsync();

      await SendOkAsync(ct);
    }

    else 
      await SendNotFoundAsync();
  }
}
 
