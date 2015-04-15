using BrewBro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewBro.Groups.Entities
{
    public class Group : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
