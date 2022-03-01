var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

builder.Services.AddSingleton<IUserPointsRepository, UserPointsRepository>();
builder.Services.AddSingleton<IUserPointsRepository, UserPointsRepository>();

builder.Configuration.AddJsonFile("appsettings.json", false, true);

if (!string.IsNullOrWhiteSpace(builder.Configuration["MongoUri"]))
    builder.Services.AddSingleton<DataBaseImplementation<Points>>(new DataBaseImplementation<Points>(builder.Configuration["MongoUri"]));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "User points API", Version = "v1" });
});

var app = builder.Build();

if(app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "User points API v1"));
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.ConfigureApi();

app.Run();