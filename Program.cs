using Blog.Data;
using Blog.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddDbContext<BlogDataContext>();
builder.Services.AddTransient<TokenService>(); //sempre cria novo

//builder.Services.AddScoped();    //Requisição
//builder.Services.AddSingleton(); //Singleton -> 1 por app


var app = builder.Build();

app.MapControllers();

app.Run();
