using ComponentProcessing.EntityModel;
using ComponentProcessing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentProcessing.DL
{
    public interface IData
    {
        public void AddDataProcessRequestDL(ProcReq pq, CompContext ct);
        public ProcRes Package_Delivery_DL(ProcReq value, CompContext ct);

        public void AddDataProcessResponseDL(ProcRes pq, CompContext ct);
        int PaymentMicro_DL(ProcRes value, CompContext ct);
    }
}
