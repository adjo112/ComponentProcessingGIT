using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComponentProcessing.Models
{
    public class CreditDetail
    {
        [Key]
        public string CreditcardNo { get; set; }

        [Required]
        public int Creditlimit { get; set; }

    }
}
