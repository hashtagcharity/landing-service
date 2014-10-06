using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class Subscription
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [JsonProperty("emailAddress")]
        [BsonElement("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty("subscribedAt")]
        [BsonElement("subscribedAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime SubscribedAt { get; set; }

        [JsonProperty("subscriptionCode")]
        [JsonIgnore]
        [BsonElement("subscriptionCode")]
        public string SubscriptionCode { get; set; }

        [JsonProperty("isUnsubscribed")]
        [JsonIgnore]
        [BsonElement("isUnsubscribed")]
        public bool IsUnsubscribed { get; set; }
    }
}