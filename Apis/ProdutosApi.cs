using Microsoft.EntityFrameworkCore;

public static class ProdutosApi
{
    public static void MapProdutosApi(this WebApplication app)
    {
        var group = app.MapGroup("/produtos");

        group.MapGet("/", async (Bancodedados db) =>
            //select * from produtos
            await db.Produtos.ToListAsync()
        );

        group.MapPost("/", async (Produto produto, Bancodedados db) =>
        {
            db.Produtos.Add(produto);
            //insert into ...   
            await db.SaveChangesAsync();

            return Results.Created($"/produtos/{produto.Id}", produto);
        });

        group.MapPut("/", async (int id, Produto produtoAlterada, Bancodedados db) =>
        {
            //select * from produto where id = ?
            var produto = await db.Produtos.FindAsync(id);
            if (produto is null)
            {
                return Results.NotFound();
            }

            produto.Nome = produtoAlterada.Nome;
            produto.Descricao = produtoAlterada.Descricao;
            produto.Preco = produtoAlterada.Preco;

            //update...
            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, Bancodedados db) =>
        {
            if (await db.Produtos.FindAsync(id) is Produto produto)
            {
                db.Produtos.Remove(produto);
                //delete from ...
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();

        });
    }
}
