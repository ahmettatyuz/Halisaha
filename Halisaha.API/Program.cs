using System.Text;
using Halisaha.API.Security;
using Halisaha.Business.Abstract;
using Halisaha.Business.Concrete;
using Halisaha.DataAccess.Abstract;
using Halisaha.DataAccess.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<IPlayerRepository, PlayerRepository>();
builder.Services.AddSingleton<IPlayerService, PlayerManager>();
builder.Services.AddSingleton<IOwnerService, OwnerManager>();
builder.Services.AddSingleton<IOwnerRepository, OwnerRepository>();
builder.Services.AddSingleton<ISessionRepository, SessionRepository>();
builder.Services.AddSingleton<ISessionService, SessionManager>();
builder.Services.AddSingleton<IReservedSessionRepository, ReservedSessionRepository>();
builder.Services.AddSingleton<IReservedSessionService, ReservedSessionManager>();
builder.Services.Configure<JwtAyarlari>(builder.Configuration.GetSection("Token"));

// burası useAuthentication() fonksiyonu içinayarlamaları yapıyor
// useAuthentication fonksiyonunun ayaları, kullanması gereken Issuer,Audience ve Secret Keyleri nereden alması gerektiğini burada tanımlıyoruz
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecretKey"] ?? string.Empty))
        };
    }
);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                 .AllowAnyMethod()
                                 .AllowAnyHeader();
                      });
});

// rotaların en başına [Authorize] yazınca buradaki değerler ile request'in headerinden gelen token kontrol ediliyor ve ona göre rota çalışmış mı oluyor ?



builder.Services.AddSwaggerGen(swagger =>
{
    swagger.EnableAnnotations();
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Sadece JWT tokeni başında Bearer olmadan yazın.",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    swagger.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});
var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// useAuthentication() komutu rotaların başında eğer [Authorize] varsa app.AddAuthentication() 'da tanımladığımız kurallara göre kontrol yapılmasını sağlıyor.
app.UseAuthentication();


app.UseAuthorization();
app.MapControllers();
app.Run();

