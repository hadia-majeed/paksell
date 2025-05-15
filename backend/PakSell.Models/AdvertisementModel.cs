using paksell;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        // Use JsonPropertyName to match the property names in your model
        [JsonPropertyName("startsOn")]
        public DateOnly StartsOn { get; set; }

        [JsonPropertyName("endsOn")]
        public DateOnly EndsOn { get; set; }

        public CityAreaModel? CityArea { get; set; }
        public List<AdvertisementFeatureModel> AdvertisementFeatures { get; set; }
        public List<AdvertisementImageModel> AdvertisementImages { get; set; }
        public AdvertisementCategoryModel? Category { get; set; }
    }


    public class AdvertisementBindingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public DateTime EndsOn { get; set; }
        public DateTime StartsOn { get; set; }
        public List<string> AdvertisementFeatures { get; set; }
        public List<string> AdvertisementImages { get; set; }
        public int Price { get; set; }

        public int PostedBy { get; set; }

        public int CategoryId { get; set; }
    }
}

