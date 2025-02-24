using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paksell
{
    public class AdvertisementFeature : INameEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string? value { get; set; }
    }
}
