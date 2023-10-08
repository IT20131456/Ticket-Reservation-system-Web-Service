namespace MongoDotnetDemo.Models
{
    public class DatabaseSettings
    {
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
        public string? CategoriesCollectionName { get; set; }
        public string? ProductsCollectionName { get; set; }

        public string? TravelersCollectionName { get; set; }     

        public string? TrainSchedulesCollectionName { get; set; }
        public string? StaffCollectionName { get; set; }
        public string? TravelAgentCollectionName { get; set; }

    }
}
