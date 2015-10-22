using GameStore.Domain.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(UserMetadata))]
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
