var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddServerSideBlazor();
var app = builder.Build();

app.UseMvcWithDefaultRoute();

app.MapBlazorHub();
app.MapGet("/", () => "Hello World!");

app.Run();
