using MyNotes.Entities;
using MyNotes.Security;

namespace MyNotes.Endpoints.Auth.Login;

public class GetUserEndpoint: Endpoint<Request, Response>{

  private DataContext _datacontext;

  public GetUserEndpoint(DataContext dataContext)
    => _datacontext = dataContext;

  public override void Configure(){
    Post("/api/login");
    AllowAnonymous();
  }

  public override async Task HandleAsync(Request request,CancellationToken ct){
    
    string password = await Hashing.ToSha(request.password);

    var user = await _datacontext.Users.FirstOrDefaultAsync(
        x=> x.email == request.email && x.password == password);

    if(user is not null){
    
      // gerar token

      await SendAsync(new Response(user.name, user.email,"toke"));
    }
    else
      SendErrorsAsync();
  

  }
}
 
