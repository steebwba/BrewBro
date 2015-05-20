using BrewBro.Core;
using BrewBro.Core.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewBro.Users.Entities
{
    [JsonObject(MemberSerialization.OptOut)]
    [BsonIgnoreExtraElements]
    public class Group : BaseEntity
    {
        [BsonIgnoreIfNull]
        [BsonIgnoreIfDefault]
        public string Name { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        public List<User> Users { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        public User Owner { get; set; }
    }
}
