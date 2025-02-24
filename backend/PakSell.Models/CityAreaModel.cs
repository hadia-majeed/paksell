using paksell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakSell.Models
{
    public class CityAreaModel : INameEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public UserModel? User { get; set; }

    }
}
