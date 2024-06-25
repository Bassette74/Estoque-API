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
//Cadastrar Produtos -------------------------------------------------------

app.MapPost("/createUser" , async (EstoqueDbContext DbContext , User newUser)=>{
    DbContext.Usuarios.Add(newUser);
    await DbContext.SaveChangesAsync();
    return Results.Created($"/createUser/{newUser.id}", newUser);
    });

     // Consultar dados do DB
 app.MapGet("/user", async (EstoqueDbContext DbContext) =>{
    var User = await DbContext.Usuarios.ToListAsync();
    return Results.Ok(User);

 });

//Atualiza Produto Existente
app.MapPut("/user/{id}", async (int id , EstoqueDbContext dbContext , User updatedUser)=>{

    //verifica Produto existente na base , conforme o id informado
    //se o produto existir na base , sera retornado  para dentro do objeto existingProduto

    var existingUser = await dbContext.Usuarios.FindAsync(id);
    if(existingUser is null){
        return Results.NotFound($"User with ID {id} not found.");

}

existingUser.UserName = updatedUser.UserName;
existingUser.email = updatedUser.email;


//SALVA NO DB
await dbContext.SaveChangesAsync();

//retorna para o cliente que invocou o endpoint
return Results.Ok(existingUser);

});




app.Run();

