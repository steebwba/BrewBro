using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewBro.Core;

namespace BrewBro.Users.Entities
{
    public class BrewHistory : BaseEntity
    {
        public User User { get; set; }

        public DateTime Date { get; set; }

        public Group Group { get; set; }
    }
}
