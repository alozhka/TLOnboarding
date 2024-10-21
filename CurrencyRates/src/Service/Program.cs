using Service;

var builder = WebApplication.CreateSlimBuilder(args);


builder.Services.AddDependencies();


var app = builder.Build();


app.UseServices();


app.Run();
