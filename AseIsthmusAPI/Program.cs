using AseIsthmusAPI.Data;
using AseIsthmusAPI.Services;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//DB Context
builder.Services.AddSqlServer<AseIsthmusContext>(builder.Configuration.GetConnectionString("AseIsthmusConn"));

//Service Layer 
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<LoginService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
