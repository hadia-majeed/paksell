using paksell;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakSell.Models
{
    public class AdvertisementFeatureModel : INameEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? value { get; set; }
    }
}
