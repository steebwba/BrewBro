using BrewBro.Core;
using BrewBro.Core.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BrewBro.Users.Entities
{
    [JsonObject(MemberSerialization.OptOut)]
    [BsonIgnoreExtraElements]
    public class User : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the profile image filename.
        /// </summary>
        /// <value>
        /// The profile image.
        /// </value>
        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        public string ProfileImage { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        public List<Group> Groups { get; set; }
    }
}
