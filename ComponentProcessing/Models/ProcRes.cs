using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComponentProcessing.Models
{
    public class ProcRes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_res { get; set; }

        [Required]
        public int RequestId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public double ProcessingCharge { get; set; }

        [Required]
        public double PackagingAndDeliveryCharge { get; set; }

        [Required]
        public DateTime DateOfDelivery { get; set; }

        [Required]
        public double TotalCharge { get; set; }

        [Required]
        public string CreditCardNo { get; set; }
    }
}
