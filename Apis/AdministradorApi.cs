using Microsoft.EntityFrameworkCore;

public static class AdministradorApi
{
    public static void MapAdministradorApi(this WebApplication app)
    {
        var group = app.MapGroup("/Administrador");

        group.MapGet("/", async (Bancodedados db) =>
            //select * from Administrador
            await db.Adm.ToListAsync()
        );

        group.MapPost("/", async (Administrador Adm, Bancodedados db) =>
        {
            db.Adm.Add(Adm);
            //insert into ...
            await db.SaveChangesAsync();

            return Results.Created($"/Administrador/{Adm.Id}", Adm);
        });

        group.MapPut("/", async (int id, Administrador AdmAlterada, Bancodedados db) =>
        {
            //select * from Adm where id = ?
            var Adm = await db.Adm.FindAsync(id);
            if (Adm is null)
            {
                return Results.NotFound();
            }

            Adm.Nome = AdmAlterada.Nome;
            Adm.CPF = AdmAlterada.CPF;
            Adm.Cargo = AdmAlterada.Cargo;
            Adm.Login = AdmAlterada.Login;

            //update...
            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, Bancodedados db) =>
        {
            if (await db.Adm.FindAsync(id) is Administrador Adm)
            {
                db.Adm.Remove(Adm);
                //delete from ...
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();

        });
    }
}
