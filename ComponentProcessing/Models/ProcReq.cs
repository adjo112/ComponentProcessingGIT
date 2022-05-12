using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComponentProcessing.Models
{
    public class ProcReq
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdReq { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public string CreditCardNumber { get; set; }
        [Required]
        public string ComponentType { get; set; }
        [Required]
        public string ComponentName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public bool IsPriorityRequest { get; set; }
        [Required]
        public DateTime OrderPlacedDate { get; set; }
    }
}
