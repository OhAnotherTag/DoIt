namespace Infrastructure.Database
{
    public class DatabaseConfig
    {
        public string Name { get; set; }
        public DatabaseConfig(string name)
        {
            Name = name;
        }
    }
}