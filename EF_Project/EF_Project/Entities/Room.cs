using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Project.Entities
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public int Price { get; set; }
        public int RoomNumber { get; set; }
        public bool Availability { get; set; }
        [StringLength(40)]
        public string RoomType { get; set; }
        public int RoomFloor { get; set; }
        public virtual List<Reservation>? Reservations { get; set; } = new();

       /* public override string ToString()
        {
            return $"{RoomNumber}";
        }*/
    }
}
