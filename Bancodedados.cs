using Microsoft.EntityFrameworkCore;

public class Bancodedados : DbContext
{

    //Configuração do banco de dados MySql
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseMySQL("server=localhost;port=3306;database=Projeto;user=root;password=");
    }

    /*protected override void OnModelCreating(ModelBuilder mb)
    {

        mb.Entity<Endereco>()
            .HasMany(e => e.Clientes)
            .WithOne(c => c.Endereco)
            .HasForeignKey(c => c.EnderecoId)
            .HasPrincipalKey(e => e.Id);

    }
    */

    //Tabelas do banco de dados
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Administrador> Adm { get; set; }
    public DbSet<Carrinho> Carrinhos { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    //...

}
