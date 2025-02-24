using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paksell
{
    public class AdvertisementCategory : INameEntity
    {

        public int Id { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string? Image { get; set; }
        public ICollection<Advertisement> Advertisements { get; set; }

    }
}
