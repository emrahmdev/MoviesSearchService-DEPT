using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using MoviesSearchService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["RedisConfiguration"];
});

builder.Services.AddSingleton<CacheService>();

builder.Services.AddSingleton(new YouTubeService(new BaseClientService.Initializer()
{
    ApiKey = builder.Configuration["YouTubeApiKey"]
}));

builder.Services.AddSingleton<ImdbSearchService>();

builder.Services.AddHttpClient<ImdbSearchService>(client =>
{
    client.BaseAddress = new Uri($"https://imdb-api.com/en/API/Search/{builder.Configuration["ImdbApiKey"]}/");
});

builder.Services.AddSingleton<YouTubeTrailerSearchService>();

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
