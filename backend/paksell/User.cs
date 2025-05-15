using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paksell
{
    public class User : INameEntity
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? Email {  get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? LoginId { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string? Password { get; set; }

        public string City { get; set; }
        [Column(TypeName = "varchar(11)")]
        public string? PhoneNumber { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string? UserImage { get; set; }

        public DateTime? BirthDate { get; set; }
      

        [Column(TypeName = "varchar(100)")]
        public string? SecurityQuestion { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? SecurityAnswer { get; set; }
    }
}
