using Domain.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;

namespace Infra.Data
{
    public class AnimalContext : DbContext
    {
        public AnimalContext() : base(@"Data Source=animalproject.database.windows.net,1433;Initial Catalog=Projetos;Persist Security Info=False;User ID=viniferraria;Password=V@197320;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
        {
        }

        //         public AnimalContext(DbContextOptions<AnimalContext> options) : base(options)
        //         {
        //             /*Database.SetInitializer<AnimalContext>(null);*/
        //         }

        public DbSet<Zoo> Zoo { get; set; }

        public virtual List<string> fromFile(string filename)
        {
            var filenameParameter = filename != null ?
                new SqlParameter("filename", filename) :
                new SqlParameter("filename", typeof(string));

            return this.Database.SqlQuery<List<string>>("fromFile @filename", filenameParameter).SingleOrDefault();
            /*((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("fromFile", filenameParameter);*/
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
