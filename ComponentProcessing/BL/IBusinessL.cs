using ComponentProcessing.EntityModel;
using ComponentProcessing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentProcessing.BL
{
   public interface IBusinessL
    {
        public CreditDetail AddDataBL(ProcReq value, CompContext ct);

        public ProcRes ProcessResponsefromPD_MicroBL(ProcReq value,CompContext ct);

        public void AddDataProcessResponseBL(ProcRes value, CompContext ct);

        public int PaymentBusiness(ProcRes value,CompContext ct);
    }
}
