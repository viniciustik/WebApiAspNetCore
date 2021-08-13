using Microsoft.EntityFrameworkCore;
using Model;
using Model.Registrations;
using System;

namespace Repository
{
  public class ContextBase : DbContext
  {
    public ContextBase()
    { }
    public ContextBase(DbContextOptions<ContextBase> opcoes) : base(opcoes)
    {

    }
    public virtual DbSet<User> User { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer(@"Server=.\sqlexpress;Database=serviceboxdb;Trusted_Connection=True;");
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      ConfiguraCompany(modelBuilder);
      ConfiguraEmpresa(modelBuilder);
      ConfiguraClient(modelBuilder);
      ConfiguraFile(modelBuilder);
      ConfiguraDescriptionFiles(modelBuilder);
      base.OnModelCreating(modelBuilder);

    }

    private void ConfiguraEmpresa(ModelBuilder builder)
    {
      builder.Entity<User>(user =>
      {
        user.ToTable("tb_user");
        user.HasKey(c => c.Id);
        user.Property(c => c.Id).ValueGeneratedOnAdd();
        user.Property(c => c.Name).HasMaxLength(100);
        user.Property(c => c.Password).HasMaxLength(150);
        user.Property(c => c.Email).HasMaxLength(100);
      });


      builder.Entity<User>()
       .HasOne(dc => dc.Company)
       .WithMany(c => c.Users)
       .HasForeignKey(dc => dc.IdCompany);

      builder.Entity<User>().HasData(new User { Id = 1, Email = "admin@padrao.com.br", Name = "Admin", Password = "", BirthDate = new DateTime(1983, 1, 1), IdCompany = 1 });
    }


    private void ConfiguraClient(ModelBuilder builder)
    {
      builder.Entity<Client>(client =>
      {
        client.ToTable("tb_client");
        client.HasKey(c => c.Id);
        client.Property(c => c.Id).ValueGeneratedOnAdd();
        client.Property(c => c.Name).HasMaxLength(150);
        client.Property(c => c.Email).HasMaxLength(100);
        client.Property(c => c.CellPhone).HasMaxLength(15);
        client.Property(c => c.Address).HasMaxLength(150);
        client.Property(c => c.Bairro).HasMaxLength(100);
        client.Property(c => c.Complement).HasMaxLength(150);
      });
    }
    private void ConfiguraCompany(ModelBuilder builder)
    {
      builder.Entity<Company>(user =>
      {
        user.ToTable("tb_company");
        user.HasKey(c => c.Id);
        user.Property(c => c.Id).ValueGeneratedOnAdd();
      });
      builder.Entity<Company>().HasData(new Company { Id = 1, CorporateName = "Empresa Padrão" });
    }
    private void ConfiguraFile(ModelBuilder builder)
    {
      builder.Entity<File>(user =>
      {
        user.ToTable("tb_file");
        user.HasKey(c => c.Id);
        user.Property(c => c.Id).ValueGeneratedOnAdd();

      });
      builder.Entity<File>()
    .HasOne(dc => dc.DescriptionFiles)
    .WithMany(c => c.Files)
    .HasForeignKey(dc => dc.IdDescriptionFiles);
    }

    private void ConfiguraDescriptionFiles(ModelBuilder builder)
    {
      builder.Entity<DescriptionFiles>(user =>
      {
        user.ToTable("tb_descriptionFiles");
        user.HasKey(c => c.Id);
        user.Property(c => c.Id).ValueGeneratedOnAdd();
      });
      builder.Entity<DescriptionFiles>()
   .HasOne(dc => dc.Company)
   .WithMany(c => c.DescriptionFiles)
   .HasForeignKey(dc => dc.idCompany);
    }

  }

}
