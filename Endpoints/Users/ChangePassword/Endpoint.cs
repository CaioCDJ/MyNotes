using MyNotes.Entities;
using MyNotes.Security;

namespace MyNotes.Endpoints.Users.ChangePassword;

public class ChangePasswordEndpoint: Endpoint<Request>{

  private DataContext _datacontext;

  public ChangePasswordEndpoint(DataContext dataContext)
    => _datacontext = dataContext;

  public override void Configure(){
    Patch("/api/user/{id}/changePassword");
    AllowAnonymous();
  }

  public override async Task HandleAsync(Request request,CancellationToken ct){
    
    string id = Route<string>("id");
    
    if(id is null || string.IsNullOrEmpty(id))  await SendErrorsAsync();
    
    var user = await _datacontext.Users.FirstOrDefaultAsync(x=>x.Id == id);

    if(user is null) await SendNotFoundAsync(ct);

    if(request.newPassword != request.confirmPasswor) await SendErrorsAsync(); 
  
    string oldPassword = await Hashing.ToSha(request.oldPassword);

    if(oldPassword == user.password){
      user.password = await Hashing.ToSha(request.newPassword);
      _datacontext.Users.Update(user);
      await _datacontext.SaveChangesAsync();
      await SendOkAsync("Senha alterada com sucesso",ct);
    }
    
    else 
      await SendErrorsAsync();

    
  }
}
 
