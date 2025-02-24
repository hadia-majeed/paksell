using paksell;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakSell.Models
{
    public class AdvertisementModel : INameEntity
    {
        public AdvertisementModel()
        {
            AdvertisementImages = new List<AdvertisementImageModel>();
            AdvertisementFeatures = new List<AdvertisementFeatureModel>();

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public UserModel? PostedBy { get; set; }
        public int Price { get; set; }

        public DateOnly startsOn { get; set; }
        public DateOnly endsOn { get; set; }
        public CityAreaModel? CityArea { get; set; }
        public List<AdvertisementFeatureModel> AdvertisementFeatures { get; set; }
        public List<AdvertisementImageModel> AdvertisementImages { get; set; }
        public AdvertisementCategoryModel? Category { get; set; }

    }
}
