/**
 * Filename: DatabaseSettings.cs
 * Namespace: MongoDotnetDemo.Models
 * Author: IT20131456,IT20128036,IT20127046,IT20125202
 * Description: This class represents the database settings for the MongoDB database used in the application.
 *              It defines various collection names and the connection details.
 */
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

         public string? TicketBookingCollectionName { get; set; }

        public string? StaffCollectionName { get; set; }
        public string? TravelAgentCollectionName { get; set; }


    }
}
