using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Models
{
    public class AccessToken
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Access_token { get; set; }
        public string Token_type { get; set; }
        public string Expires_at { get; set; }
        public string Expires_in { get; set; }
        public string Refresh_token { get; set; }
    }
}
