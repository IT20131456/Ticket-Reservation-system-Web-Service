using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDotnetDemo.Models
{
    public class TrainSchedule
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        public required string? train_number { get; set; }

        public string? train_name { get; set; }

        public string? train_type { get; set; }

        public string? train_description { get; set; }

        public string? departure_station { get; set; }

        public string? arrival_station { get; set; }

        public string? departure_time { get; set; }

        public string? arrival_time { get; set; }

        public string? travel_duration { get; set; }

        public List<string>? intermediate_stops { get; set; }

        public List<string>? seat_classes { get; set; }

        public List<string>? number_of_seats { get; set; }

        public int? isActive { get; set; }

    }

}

