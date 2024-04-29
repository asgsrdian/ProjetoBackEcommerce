using Microsoft.EntityFrameworkCore;

public static class EnderecosApi
{
    public static void MapEnderecosApi(this WebApplication app)
    {
        var group = app.MapGroup("/Endereco");

        group.MapGet("/", async (Bancodedados db) =>
            //select * from Endereco
            await db.Enderecos.ToListAsync()
        );

        group.MapPost("/", async (Endereco Enderecos, Bancodedados db) =>
        {
            db.Enderecos.Add(Enderecos);
            
            //insert into ...
            await db.SaveChangesAsync();

            return Results.Created($"/Endereco/{Enderecos.Id}", Enderecos);
        });

        group.MapPut("/", async (int id, Endereco EnderecosAlterada, Bancodedados db) =>
        {
            //select * from Enderecos where id = ?
            var Enderecos = await db.Enderecos.FindAsync(id);
            if (Enderecos is null)
            {
                return Results.NotFound();
            }

            Enderecos.Rua = EnderecosAlterada.Rua;
            Enderecos.Numero = EnderecosAlterada.Numero;
            Enderecos.Bairro = EnderecosAlterada.Bairro;

            //update...
            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, Bancodedados db) =>
        {
            if (await db.Enderecos.FindAsync(id) is Endereco Enderecos)
            {
                db.Enderecos.Remove(Enderecos);
                //delete from ...
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();

        });
    }
}