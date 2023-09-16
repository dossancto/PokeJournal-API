using System.Text;

using PokeJournal.Data;
using PokeJournal.Middlewares;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using DotNetEnv;
Env.Load();

var builder = WebApplication.CreateBuilder(args);

string getConnectionString()
{
    string? envConnectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");

    if (envConnectionString != null)
    {
        Console.WriteLine("Using Environment database");
        return envConnectionString;
    }

    Console.WriteLine("Using development database");
    return builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connect string not found");
}

var connectionString = getConnectionString();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(connectionString,
        new MySqlServerVersion(new Version(8, 0, 26))));


string jwtSecretKey = Environment.GetEnvironmentVariable("JWY_SECRET_KEY") ?? throw new InvalidOperationException("\"JWT Sercret key\" Not founded. Pleace check the env variable");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Set to true in production
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "",
        ValidAudience = "",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseAuthMiddleWare();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // TODO: Change to /DevError and create this route
    app.UseExceptionHandler("/Error");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
