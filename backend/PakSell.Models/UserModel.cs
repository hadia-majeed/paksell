using paksell;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakSell.Models
{
    public class UserModel : INameEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }

        public string City { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserImage { get; set; }

        public DateTime? BirthDate { get; set; }
        public List<AdvertisementModel> Advertisements { get; set; }
        public string? SecurityQuestion { get; set; }
        public string? SecurityAnswer { get; set; }
    }
}
