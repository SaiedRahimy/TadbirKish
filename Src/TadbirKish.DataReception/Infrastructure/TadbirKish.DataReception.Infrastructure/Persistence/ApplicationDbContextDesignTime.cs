using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TadbirKish.DataReception.Infrastructure.Persistence
{
    public class ApplicationDbContextDesignTime : IDesignTimeDbContextFactory<DataReceptionContext>
    {
        private readonly string Env = "Development";
        private string ConnectionString = "Server=.;Database=TadbirKish_DataReceptionitectureDb;Integrated Security=true;TrustServerCertificate=True";

        private void SetEnvironment()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build();
            //Env = config.GetSection("Environment").Value;
        }

        public DataReceptionContext CreateDbContext(string[] args)
        {
            //SetEnvironment();

            //var envConfig = new ConfigurationBuilder()
            //    .AddJsonFile($"appsettings.{Env}.json", optional: true).Build();
            var optionsBuilder = new DbContextOptionsBuilder<DataReceptionContext>();

            //Console.WriteLine($"connection {envConfig.GetConnectionString("BusinessDataConnectionString")}");
            //optionsBuilder.UseSqlServer(envConfig.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseSqlServer(ConnectionString);

            return new DataReceptionContext(optionsBuilder.Options);
        }
    }
}
