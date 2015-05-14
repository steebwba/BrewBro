using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewBro.Core.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
        bool Selected { get; set; }
        bool Deleted { get; set; }
    }
}
