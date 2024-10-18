var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
}

app.UseRouting();
app.MapControllers();

app.Run();
