using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Entities
{
    [ExcludeFromCodeCoverage]

    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }


        public ICollection<OrderDetail> OrderDetails { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}
