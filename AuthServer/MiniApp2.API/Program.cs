using SharedLibrary.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ?? Token Auth + Authorization ayarlar�
builder.AddTokenAuth();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ?? S�ra �nemli: �nce authentication, sonra authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
