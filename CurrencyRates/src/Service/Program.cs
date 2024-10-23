using Service;

var builder = WebApplication.CreateSlimBuilder(args);


builder.Services.AddDependencies(builder.Configuration);


var app = builder.Build();


app.UseServices();


app.Run();

public partial class Program { }