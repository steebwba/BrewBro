using BrewBro.Core;
using BrewBro.Core.Interfaces;
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
        public string Name { get; set; }

        List<User> Users { get; set; }
    }
}
