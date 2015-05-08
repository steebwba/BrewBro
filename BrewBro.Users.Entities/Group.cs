using BrewBro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewBro.Users.Entities
{
    public class Group : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Selected { get; set; }
        public bool Deleted { get; set; }
    }
}
