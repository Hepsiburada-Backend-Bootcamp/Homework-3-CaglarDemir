using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Entities
{
    [ExcludeFromCodeCoverage]

    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string FullName { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
