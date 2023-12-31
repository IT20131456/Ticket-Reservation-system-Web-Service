/*
 * Filename: TicketBooking.cs
 * Author: IT20131456 
 * Description: Model class representing a ticket booking in the Ticket Booking API.
 *              Defines the structure of a ticket booking including reservation details, train information, and pricing.
 */
 
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace MongoDotnetDemo.Models
{
    public class TicketBooking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string reservation_number { get; set; }
        public string reference_id { get; set; }
        public string train_id { get; set; }
        public string train_name { get; set; }
        public string travel_route { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string booking_date { get; set; }
        public string reservation_date { get; set; }    
        public int ticket_class { get; set; }
        public int number_of_tickets { get; set; }
        public int total_price { get; set; }

         public string status { get; set; }
    }
}
