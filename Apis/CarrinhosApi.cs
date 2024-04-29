using Microsoft.EntityFrameworkCore;

public static class CarrinhosApi
{
    public static void MapCarrinhosApi(this WebApplication app)
    {
        var group = app.MapGroup("/Carrinho");

        group.MapGet("/", async (Bancodedados db) =>
            //select * from Carrinho
            await db.Carrinhos.ToListAsync()
        );

        group.MapPost("/", async (Carrinho Carrinhos, Bancodedados db) =>
        {
            db.Carrinhos.Add(Carrinhos);
            //insert into ...
            await db.SaveChangesAsync();

            return Results.Created($"/Carrinho/{Carrinhos.Id}", Carrinhos);
        });

        group.MapPut("/", async (int id, Carrinho CarrinhosAlterada, Bancodedados db) =>
        {
            //select * from Carrinhos where id = ?
            var Carrinhos = await db.Carrinhos.FindAsync(id);
            if (Carrinhos is null)
            {
                return Results.NotFound();
            }

            Carrinhos.Nome = CarrinhosAlterada.Nome;
            Carrinhos.Descricao = CarrinhosAlterada.Descricao;
            Carrinhos.Preco = CarrinhosAlterada.Preco;

            //update...
            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, Bancodedados db) =>
        {
            if (await db.Carrinhos.FindAsync(id) is Carrinho Carrinhos)
            {
                db.Carrinhos.Remove(Carrinhos);
                //delete from ...
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();

        });
    }
}