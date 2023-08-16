global using Microsoft.EntityFrameworkCore;
global using MyNotes.Data;
global using FastEndpoints;
global using FastEndpoints.Security;
using FastEndpoints.Swagger;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddFastEndpoints();
builder.Services.AddJWTBearerAuth("definitivamenteumasenhaparatoken");

builder.Services.SwaggerDocument(x=>{
    x.DocumentSettings = doc =>{
      doc.Title = "MyNotes Api Doc";
      doc.Version =  "v1";
    };
  });

builder.Services.AddDbContext<DataContext>(
  options => options.UseSqlite("DataSource=app.db;Cache=Shared")
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDefaultExceptionHandler(); //add this;
app.UseRouting();
app.UseAuthentication();
app.UseFastEndpoints();
app.UseSwaggerGen();

/*
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
*/

app.MapFallbackToFile("index.html");

app.Run();
