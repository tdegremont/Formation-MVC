using FirstMVCApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstMVCApp.Services.DALEmployes
{
    /// <summary>
    /// Permet d'accéder aux données relatives aux employés
    /// </summary>
    public class EmployeDbContext : DbContext
    {
        private readonly List<Employe> initialData;

        // Ce constructeur demande les options spécifiques à ce context
        // et les passe aux contructeur de la base (DbContext)
        public EmployeDbContext(DbContextOptions<EmployeDbContext> options,
            List<Employe> initialData
            ):base(options)
        {
            this.initialData = initialData;
        }
        public DbSet<EmployeDAO> Employes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Permet de spécifier les caractéristiques de la BDD
            base.OnModelCreating(modelBuilder);

            // Fluent API => Ensemble de fonctions qui sont utilisées comme un flux
            modelBuilder.Entity<EmployeDAO>(options =>
            {
                // Définir le nom de la table
                options.ToTable("Tbl_Employes");
                // Clé primaire
                options.HasKey(c => c.Id).IsClustered(false);

                // Définir le nom de la colonne associée à Id
                options.Property(c => c.Id).HasColumnName("PK_Employe");

                // Création d'index cluster
                options.HasIndex(c => new { c.Name, c.Prenom }).HasFillFactor(40).IsClustered(true);

                // Specification des longueurs des colonnes
                options.Property(c => c.Name).HasMaxLength(50).HasColumnName("Nom");
                options.Property(c => c.Prenom).HasMaxLength(50);

                // Création de DAOS à partir de la liste des employés initiale
                var initialDAOs = initialData.Select(e => new EmployeDAO()
                {
                    Name = e.Nom,
                    Prenom = e.Prenom,
                    Actif = e.Actif,    
                    DateEntree = e.DateEntree,
                    Matricule= e.Matricule,
                    Salaire= e.Salaire,
                    DerniereModif=DateTime.Now,
                    Confidentiel="Toto"

                }).ToList();

                // Insertion des données initiales des employés
                modelBuilder.Entity<EmployeDAO>().HasData(initialDAOs);

            });


        }
    }
}
