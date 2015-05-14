using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewBro.Core
{
    [JsonObject(MemberSerialization.OptOut)]
    public class BaseEntity : BrewBro.Core.Interfaces.IEntity
    {
        /// <summary>
        /// Gets or sets the id for this object (the primary record for an entity).
        /// </summary>
        /// <value>
        /// The id for this object (the primary record for an entity).
        /// </value>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BaseEntity"/> is selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if selected; otherwise, <c>false</c>.
        /// </value>
        public bool Selected { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BaseEntity"/> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Deleted { get; set; }
    }
}
