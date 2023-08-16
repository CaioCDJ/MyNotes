using MyNotes.Entities;
using MyNotes.Security;

namespace MyNotes.Endpoints.Users.Create;

public class CreateUserEndpoint: Endpoint<Request,string>{

  private DataContext _datacontext;

  public CreateUserEndpoint(DataContext dataContext)
    =>_datacontext = dataContext;

  public override void Configure(){
    Verbs(Http.GET);
    Post("/api/user/");
    AllowAnonymous();
  }

  public override async Task HandleAsync(Request request,CancellationToken ct){
  
    if(request is null ){
      await SendErrorsAsync();
    }

    string password = await Hashing.ToSha(request.password);

    _datacontext.Users.Add(new User(){
        name = request.name,
        password = password,
        email = request.email
        });
    
    try{
      await _datacontext.SaveChangesAsync();
      await SendOkAsync("deu good",ct);
    }
    catch (System.Exception){ 
      throw;
    }

  }
}
 
