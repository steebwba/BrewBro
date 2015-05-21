using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewBro.Core;
using MongoDB.Bson.Serialization.Attributes;

namespace BrewBro.Users.Entities
{
    [BsonIgnoreExtraElements]
    public class BrewHistory : BaseEntity
    {
        public User User { get; set; }

        public DateTime Date { get; set; }

        public Group Group { get; set; }
    }
}
