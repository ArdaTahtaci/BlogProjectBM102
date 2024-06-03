using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlogProject.Entities{
            [BsonIgnoreExtraElements]
    public class User{
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string _id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Post[] Posts { get; set; }
    }
}
