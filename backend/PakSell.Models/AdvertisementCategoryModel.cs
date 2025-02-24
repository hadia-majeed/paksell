using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using paksell;

namespace PakSell.Models
{
    public class AdvertisementCategoryModel : INameEntity
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public int AdvertisementCount { get; set; }
        public List<AdvertisementModel> Advertisements { get; set; }


    }
}
