using ComponentProcessing.DL;
using ComponentProcessing.EntityModel;
using ComponentProcessing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentProcessing.BL
{
    public class BusinessL:IBusinessL
    {

       
        private IData _idata;
        public BusinessL(IData idata) {
            _idata = idata;
        }

        public void AddDataBL(ProcReq value, CompContext ct)
        {
            //Data obj = new Data();
            //obj.AddData(value, ct);
            _idata.AddDataProcessRequestDL(value, ct);
        }

        public void AddDataProcessResponseBL(ProcRes value, CompContext ct)
        {
            _idata.AddDataProcessResponseDL(value, ct);
        }

        public int PaymentBusiness(ProcRes value,CompContext ct)
        {
            return _idata.PaymentMicro_DL(value,ct);
        }

        public ProcRes ProcessResponsefromPD_MicroBL(ProcReq value, CompContext ct)
        {
            return _idata.Package_Delivery_DL(value, ct);
        }
    }
}
