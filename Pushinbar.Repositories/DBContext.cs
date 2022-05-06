using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Pushinbar.Common.Entities;

namespace Pushinbar.Repositories
{
    public sealed class DBContext : DbContext
    {
        private readonly string connectionString;
        
        public DbSet<AlcoholEntity> Alcohol { get; set; }
        public DbSet<NotAlcoholEntity> NotAlcohol { get; set; }
        public DbSet<EatEntity> Eat { get; set; }
        public DbSet<SnackEntity> Snacks { get; set; }
        
        public DBContext(string connectionString)
        {
            this.connectionString = connectionString;
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString, builder =>
            {
                builder.RemoteCertificateValidationCallback((s, c, ch, sslPolicyErrors) =>
                {
                    if (sslPolicyErrors == SslPolicyErrors.None)
                    {
                        return true;
                    }

                    return false;
                });
                builder.ProvideClientCertificatesCallback(clientCerts =>
                {
                    var clientCertPath = "/home/berr-menu/.postgresql/root.crt";
                    // To avoid permission ex run: "sudo chmod -R 777 /home/username/.postgresql/root.crt"
                    var cert = new X509Certificate2(clientCertPath);
                    clientCerts.Add(cert);
                });
            });
        }
    }
}