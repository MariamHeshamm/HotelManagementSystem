using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Project.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Firstname { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Lastname { get; set; }
        
        [Required]
        public DateTime Birthdate { get; set; }
        
        [Required]
        [StringLength(10)]
        public string gender { get; set; }
       
        [Required]
        [StringLength(15)]
        public string Phonenumber { get; set; }
        
        [Required]
        [StringLength(50)]
        public string EmailAddress { get; set; }
       
        [Required]
        [StringLength(40)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public int ApartementSuite {get;set;}

        [Required]
        [StringLength(10)]
        public string Zipcode { get; set; }

        [Required]
        [StringLength(50)]
        public string StreetAddress { get; set; }
        public virtual State? State { get; set; }
        public virtual List<Reservation>? Reservations { get; set; } = new();
        public override string ToString()
        {
            return $"{CustomerId} | {Firstname} {Lastname } | {Phonenumber}";
        }
    }
}
