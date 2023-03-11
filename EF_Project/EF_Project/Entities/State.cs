using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Project.Entities
{
    public class State
    {
        [Key]
        public int StatesId { get; set; }
        [StringLength(50)]
        public string StatesName { get; set;}
        public virtual List<Customer> Customers { get; set; } = new();
    }
}
