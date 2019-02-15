using System.Data.Entity;

namespace UploadingToWebApi.Web.Models
{
    public class PersonDataModel : DbContext
    {
        // Your context has been configured to use a 'PersonDataModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'UploadingToWebApi.Web.Models.PersonDataModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'PersonDataModel' 
        // connection string in the application configuration file.
        public PersonDataModel()
            : base("name=PersonDataModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Person> People { get; set; }
    }
}