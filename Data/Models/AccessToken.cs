using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Models
{
    internal class AccessToken
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_at { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
    }
}
