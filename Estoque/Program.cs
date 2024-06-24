using Estoque;
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


var app = builder.Build();


//Cadastrar Produtos -------------------------------------------------------

app.MapPost("/createProduto" , async (EstoqueDbContext DbContext , Produto newProduto)=>{
    DbContext.Produtos.Add(newProduto);
    await DbContext.SaveChangesAsync();
    return Results.Created($"/createProduto/{newProduto.Id}", newProduto);
    });

     // Consultar dados do DB
 app.MapGet("/produtos", async (EstoqueDbContext DbContext) =>{
    var produtos = await DbContext.Produtos.ToListAsync();
    return Results.Ok(produtos);

 });

//Atualiza Produto Existente
app.MapPut("/Produto/{id}", async (int id , EstoqueDbContext dbContext , Produto updatedProduto)=>{

    //verifica Produto existente na base , conforme o id informado
    //se o produto existir na base , sera retornado  para dentro do objeto existingProduto

    var existingProduto = await dbContext.Produtos.FindAsync(id);
    if(existingProduto is null){
        return Results.NotFound($"Produto with ID {id} not found.");

}

existingProduto.Nome = updatedProduto.Nome;
existingProduto.Preco = updatedProduto.Preco;
existingProduto.fornecedor = updatedProduto.fornecedor;

//SALVA NO DB
await dbContext.SaveChangesAsync();

//retorna para o cliente que invocou o endpoint
return Results.Ok(existingProduto);

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

