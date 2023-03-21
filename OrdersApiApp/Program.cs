using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using OrdersApiApp.Model;
using OrdersApiApp.Model.Entity;
using OrdersApiApp.Service.ClientService;
using OrdersApiApp.Service.OrderProductsService;
using OrdersApiApp.Service.OrderService;
using OrdersApiApp.Service.ProductService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// добавление зависимостей
builder.Services.AddTransient<IDaoClient, DbDaoClient>();
builder.Services.AddTransient<IDaoOrder, DbDaoOrder>();
builder.Services.AddTransient<IDaoProduct, DbDaoProduct>();
builder.Services.AddTransient<IDaoOrderProducts, DbDaoOrderProducts>();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = AuthOptions.ISSUER,
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = AuthOptions.AUDIENCE,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    });


builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddCors();



var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/login/{username}", async (string username, HttpContext context, IDaoClient dao) =>
{

    Client? client = await dao.GetClientByUsername(username);


    var claims = new List<Claim> { new Claim(ClaimTypes.Name, client.Name) };
    var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)), // время действия 2 минуты
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

    var encodedJwt =  new JwtSecurityTokenHandler().WriteToken(jwt);

    var response = new
    {
        access_token = encodedJwt,
        username = client.Name
    };

    return Results.Json(response);
});


/// CLients routes

app.MapGet("/clients/all", async (HttpContext context, IDaoClient dao) =>
{
    return await dao.GetAllClients();
});
app.MapGet("/clients/{id}", async (int id, HttpContext context, IDaoClient dao) =>
{
    return await dao.GetClientById(id);
});

app.MapPost("/clients/add", async (HttpContext context, Client client, IDaoClient dao) =>
{
    return await dao.AddClient(client);
});

app.MapPost("/clients/update", async (HttpContext context, Client updatedClient, IDaoClient dao) =>
{
    return await dao.UpdateClient(updatedClient);
});

app.MapPost("/clients/delete/{id}", async (int id, HttpContext context, IDaoClient dao) =>
{
    return await dao.DeleteClient(id);
});

/// Orders routes

app.MapPost("/orders/add", async (HttpContext context, Order order, IDaoOrder dao) =>
{
    return await dao.AddOrder(order);
});

app.MapGet("/orders/all", [Authorize] async (HttpContext context, IDaoOrder dao) =>
{
    return await dao.GetAllOrders();
});

app.MapPost("/orders/update", async (HttpContext context, Order updatedOrder, IDaoOrder dao) =>
{
    return await dao.UpdateOrder(updatedOrder);
});

app.MapGet("/orders/{id}", async (int id,HttpContext context, IDaoOrder dao) =>
{
    return await dao.GetOrderById(id);
});

app.MapPost("/orders/delete/{id}", async (int id, HttpContext context, IDaoOrder dao) =>
{
    return await dao.DeleteOrder(id);
});

app.MapGet("/orders/{id}/info", async (int id, HttpContext context, IDaoOrder dao) =>
{
    return await dao.GetOrderInfo(id);
});

/// Products routes

app.MapPost("/products/add", async (HttpContext context, Product product, IDaoProduct dao) =>
{
    return await dao.AddProduct(product);
});

app.MapGet("/products/all", async (HttpContext context, IDaoProduct dao) =>
{
    return await dao.GetAllProducts();
});

app.MapPost("/products/update", async (HttpContext context, Product updatedProduct, IDaoProduct dao) =>
{
    return await dao.UpdateProduct(updatedProduct);
});

app.MapGet("/products/{id}", async (int id, HttpContext context, IDaoProduct dao) =>
{
    return await dao.GetProductById(id);
});

app.MapPost("/products/delete/{id}", async (int id, HttpContext context, IDaoProduct dao) =>
{
    return await dao.DeleteProduct(id);
});

/// OrderProducts routes

app.MapPost("/orderProducts/add", async (HttpContext context, OrderProducts orderProducts, IDaoOrderProducts dao) =>
{
    return await dao.AddOrderProducts(orderProducts);
});

app.MapGet("/orderProducts/all", async (HttpContext context, IDaoOrderProducts dao) =>
{
    return await dao.GetAllOrderProducts();
});

app.MapPost("/orderProducts/update", async (HttpContext context, OrderProducts updatedorderProducts, IDaoOrderProducts dao) =>
{
    return await dao.UpdateOrderProducts(updatedorderProducts);
});

app.MapGet("/orderProducts/{id}", async (int id, HttpContext context, IDaoOrderProducts dao) =>
{
    return await dao.GetOrderProductsById(id);
});

app.MapPost("/orderProducts/delete/{id}", async (int id, HttpContext context, IDaoOrderProducts dao) =>
{
    return await dao.DeleteOrderProducts(id);
});

app.Run();

public class AuthOptions
{
    public const string ISSUER = "MyAuthServer"; // издатель токена
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}