using Estoque;
using loja.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Update.Internal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurando a conexão com o banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EstoqueDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 26))));

//Cadastra os serviços ao conteiner

builder.Services.AddScoped<ProductService>();

var app = builder.Build();


// Configuração das requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


                                   
//Cadastrar Produtos ------------------UTILIZANDO SERVICE-------------------------------------

app.MapGet("/produtos", async (ProductService productService) =>
{
    var produtos = await productService.GetAllProductsAsync();
    return Results.Ok(produtos);
});

app.MapGet("/produtos/{id}", async (int id, ProductService productService) =>
{
    var produto = await productService.GetProductByIdAsync(id);
    if (produto == null)
    {
        return Results.NotFound($"Product with ID {id} not found.");
    }
    return Results.Ok(produto);
});

app.MapPost("/produtos", async (Produto produto, ProductService productService) =>
{
    await productService.AddProductAsync(produto);
    return Results.Created($"/produtos/{produto.Id}", produto);
});

app.MapPut("/produtos/{id}", async (int id, Produto produto, ProductService productService) =>
{
    if (id != produto.Id)
    {
        return Results.BadRequest("Product ID mismatch.");
    }

    await productService.UpdateProductAsync(produto);
    return Results.Ok();
});

app.MapDelete("/produtos/{id}", async (int id, ProductService productService) =>
{
    await productService.DeleteProductAsync(id);
    return Results.Ok();
});


//---------------------------------------------Cadastro usuarios --------------------------------------------------------


app.MapGet("/user", async (UserService userService) =>
{
    var user = await userService.GetAllUserAsync();
    return Results.Ok(user);
});

app.MapGet("/user/{id}", async (int id, UserService userService) =>
{
    var user = await userService.GetPUserByIdAsync(id);
    if (user == null)
    {
        return Results.NotFound($"User with ID {id} not found.");
    }
    return Results.Ok(user);
});

app.MapPost("/user", async (User user, UserService GetPUserByIdAsync) =>
{
    await GetPUserByIdAsync.AddUserAsync(user);
    return Results.Created($"/produtos/{user.id}", user);
});

app.MapPut("/user/{id}", async (int id, User user, UserService userService) =>
{
    if (id != user.id)  // Corrigido 'user.id' para 'user.Id' para seguir a convenção de PascalCase.
    {
        return Results.BadRequest("User ID mismatch.");
    }

    await userService.UpdateUserAsync(user);  // Chamando o método na instância de userService.
    return Results.Ok();
});

app.MapDelete("/user/{id}", async (int id, UserService userService) =>
{
    await userService.DeleteUserAsync(id);
    return Results.Ok();
});
 //------------------------Endpoint estoque--------------------------------------------

 app.MapPost("/produtos/{id}/mover-para-estoque", async (int id, int quantidade, EstoqueService estoqueService) =>
{
    try
    {
        await estoqueService.MoverProdutoParaEstoqueAsync(id, quantidade);
        return Results.Ok();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});


app.Run();

