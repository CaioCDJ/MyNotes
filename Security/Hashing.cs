using System.Text;
using System.Security.Cryptography;

namespace MyNotes.Security;

public class Hashing{

  public async static Task<string> ToSha(string text){

    using var sha = SHA256.Create();
    byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(text));

    StringBuilder builder = new StringBuilder();

    for(int i = 0; i < bytes.Length ;i++){
      builder.Append(bytes[i].ToString("x2"));
    }
    return builder.ToString();
  }

  public async static Task<bool>Compare(string passoword, string text){
    string compareText = await ToSha(text);
    
    return (compareText == passoword);
  }

}
