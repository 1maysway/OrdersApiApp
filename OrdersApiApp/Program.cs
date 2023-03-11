using OrdersApiApp.Model;
using OrdersApiApp.Model.Entity;
using OrdersApiApp.Service.ClientService;
using OrdersApiApp.Service.OrderProductsService;
using OrdersApiApp.Service.OrderService;
using OrdersApiApp.Service.ProductService;

var builder = WebApplication.CreateBuilder(args);

// добавление зависимостей
builder.Services.AddTransient<IDaoClient, DbDaoClient>();
builder.Services.AddTransient<IDaoOrder, DbDaoOrder>();
builder.Services.AddTransient<IDaoProduct, DbDaoProduct>();
builder.Services.AddTransient<IDaoOrderProducts, DbDaoOrderProducts>();


builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddCors();



var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));

app.MapGet("/", () => "Hello World!");


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

app.MapGet("/orders/all", async (HttpContext context, IDaoOrder dao) =>
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
