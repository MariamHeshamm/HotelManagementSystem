using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Project.Entities
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public bool Cleaning { get; set; }
        public bool Towels { get; set; }
        public bool SweetestSurprise { get; set; }
        
        [Required]
        [Column(TypeName = "date")]
        public DateTime? ArrivalTime { get; set; }
        
        [Required]
        [Column(TypeName = "date")]
        public DateTime? LeavingTime { get; set; }
        public int Number_of_Guests { get; set; }
      
        public int BreakFast { get; set; }
        public int Launch { get; set; }
        public int Dinner { get; set; }
        public int Food_Bill { get; set; }

        public bool Credit { get; set; }
        public bool Debit { get; set; }
        [Required]
        [StringLength(50)]
        public string Card_Number { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime Card_ExpDate { get; set; }
        public Nullable<bool> Check_In { get; set; }
        public Nullable< bool> Supply_Status { get; set; }
        [Required]
        [StringLength(10)]
        public string Card_Cvv { get; set; }
        public int Total_Bill { get; set; }
        [Required]
        [StringLength(50)]
        public string Card_Type { get; set; }
        public virtual Room Room { get; set; }
        public virtual Customer Customer { get; set; }

        public override string ToString()
        {
            return $"{Customer.CustomerId }|{Customer.Firstname }{Customer.Lastname }| {Room.RoomNumber }|{Customer.Phonenumber}";
        }
    }
}
