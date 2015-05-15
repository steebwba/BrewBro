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
    public class Group : BaseEntity
    {
        public Group()
        {
            Users = new List<User>();
        }
        public string Name { get; set; }

        [JsonProperty("Users")]
        [BsonIgnoreIfNull]
        List<User> Users { get; set; }
    }
}
