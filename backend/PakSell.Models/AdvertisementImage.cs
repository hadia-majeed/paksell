using paksell;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakSell.Models
{
    public class AdvertisementImageModel : IEntity
    {
        public int Id { get; set; }
        public int Rank { get; set; }

        public string? ImagePath { get; set; }


    }
}
