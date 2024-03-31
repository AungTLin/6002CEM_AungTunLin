
using Fungry.API.Data;
using Fungry.API.Endpoints;
using Fungry.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Food");// this two both line is connected form the appsettings.json file.
builder.Services.AddDbContext<Fungry.API.Data.DataContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

    .AddJwtBearer(jwtOptions =>
        jwtOptions.TokenValidationParameters = TokenService.GetTokenValidationParameters(builder.Configuration)
    );
builder.Services.AddAuthorization();

builder.Services.AddTransient<TokenService>()
                .AddTransient<PasswordService>()
                .AddTransient<AuthService>()
                .AddTransient<FoodService>()
                .AddTransient<OrderService>();




var app = builder.Build();

#if DEBUG
MigrateDatabase(app.Services);
#endif

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapEndpoints();


app.Run();

static void MigrateDatabase(IServiceProvider sp)
{
    var scope = sp.CreateScope();
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    if (dataContext.Database.GetPendingMigrations().Any())
        dataContext.Database.Migrate();
}

