using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewBro.Core;

namespace BrewBro.Brew.Entities
{
    public class BrewHistory : BaseEntity
    {
        public Guid User { get; set; }

        public DateTime Date { get; set; }

        public Guid Group { get; set; }
    }
}
