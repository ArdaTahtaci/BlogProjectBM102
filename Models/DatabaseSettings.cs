namespace BlogProject.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string PostsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        // public string AuthMechanism { get; set; } // Yeni eklenen özellik

        // Constructor
        public DatabaseSettings(string connectionString,string databaseName)
        {
            ConnectionString = connectionString;
            // AuthMechanism = authMechanism;
            DatabaseName = databaseName;
            UsersCollectionName = "User";
            PostsCollectionName = "Post";
        }
    }

    public interface IDatabaseSettings
    {
        string UsersCollectionName { get; set; }
        string PostsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        // string AuthMechanism { get; set; }
    }
}
