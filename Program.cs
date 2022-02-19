var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/secret", () =>
{

    //to create the environment variable as a secret in k8s use: kubectl create secret generic k8ssecret --from-literal=secret="p55w0rd!"
    var secretFromEnv = Environment.GetEnvironmentVariable("secret");    
    return secretFromEnv ?? "No secret found :(";
});

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
