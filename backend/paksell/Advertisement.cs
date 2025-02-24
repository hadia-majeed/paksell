using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paksell
{
    public class Advertisement: INameEntity
    {
        public Advertisement() { 
            AdvertisementImages = new List<AdvertisementImage>();
            AdvertisementFeatures = new List<AdvertisementFeature>();

        }
        
        public int Id { get; set; }
        [Column(TypeName = "varchar(255)")]

        public string Name { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        public string? Description { get; set; }
        public int PostedById { get; set; }

        public virtual User PostedBy { get; set; }
        public int Price { get; set; }

        public DateOnly startsOn { get; set; }
        public DateOnly endsOn { get; set; }
        public virtual CityArea CityArea { get; set; }
        public virtual ICollection<AdvertisementFeature> AdvertisementFeatures { get; set; }
        public virtual ICollection<AdvertisementImage> AdvertisementImages { get; set; }
        public virtual AdvertisementCategory Category { get; set; }
    }
}
