using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pharmacy.Application.CompanyApp
{
    public class CompanyDto
    {
        [Key]
        [JsonPropertyName("companyId")]
        public int CompanyId { get; set; }
        [Required]
        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; }
    }
}
