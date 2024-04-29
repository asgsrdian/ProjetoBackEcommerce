using Microsoft.EntityFrameworkCore;

public static class ClientesApi
{
    public static void MapClientesApi(this WebApplication app)
    {
        var group = app.MapGroup("/Clientes");

        group.MapGet("/", async (Bancodedados db) =>
            //select * from Clientes
            await db.Clientes.ToListAsync()
        );

        group.MapPost("/", async (Cliente Cliente, Bancodedados db) =>
        {
            db.Clientes.Add(Cliente);
            //insert into ...
            await db.SaveChangesAsync();

            return Results.Created($"/Clientes/{Cliente.Id}", Cliente);
        });

        group.MapPut("/", async (int id, Cliente ClienteAlterada, Bancodedados db) =>
        {
            //select * from Cliente where id = ?
            var Cliente = await db.Clientes.FindAsync(id);
            if (Cliente is null)
            {
                return Results.NotFound();
            }

            Cliente.Nome = ClienteAlterada.Nome;
            Cliente.CPF = ClienteAlterada.CPF;

            //update...
            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, Bancodedados db) =>
        {
            if (await db.Clientes.FindAsync(id) is Cliente Cliente)
            {
                db.Clientes.Remove(Cliente);
                //delete from ...
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();

        });
    }
}
