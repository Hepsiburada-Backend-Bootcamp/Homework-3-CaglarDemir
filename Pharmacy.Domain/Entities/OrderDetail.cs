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

    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int MedicineId { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }



        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("MedicineId")]
        public Medicine Medicine { get; set; }

    }
}
