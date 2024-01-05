using DiagnosticWebAPIServer.Infrastructure;
using DiagnosticWebAPIServer.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<MedicalDirectoryContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MedicalDirectoryDB")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<MedicalDirectoryRepository>();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();
app.Run();
