using ComponentProcessing.EntityModel;
using ComponentProcessing.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ComponentProcessing.DL
{
    public class Data : IData
    {
        public ProcRes PopulateProcRes(string pdcharge, ProcReq value) {
            ProcRes res = new ProcRes();
            try
            {
                
                int delivery_packaging_charges = Int32.Parse(pdcharge);
                var random = new Random();
                res.RequestId = random.Next(9999, 99999);
                res.UserName = value.UserName;
                res.CreditCardNo = value.CreditCardNumber;
                res.PackagingAndDeliveryCharge = delivery_packaging_charges;
                if (value.IsPriorityRequest && value.ComponentName.Trim().ToUpper() == "REPAIR")
                {
                    res.ProcessingCharge = Price.PriorityRepair * value.Quantity;

                    res.DateOfDelivery = value.OrderPlacedDate.AddDays(2);
                }
                else if (value.IsPriorityRequest == false && value.ComponentName.Trim().ToUpper() == "REPAIR")
                {
                    res.ProcessingCharge = Price.NoPriorityRepair * value.Quantity;

                    res.DateOfDelivery = value.OrderPlacedDate.AddDays(5);
                }
                else
                {
                    res.ProcessingCharge = Price.Replacement * value.Quantity;

                    res.DateOfDelivery = value.OrderPlacedDate.AddDays(5);
                }
                res.TotalCharge = res.ProcessingCharge + res.PackagingAndDeliveryCharge;
            }
            catch (Exception e) { }
            return res;

        }
        
        
        
        public void AddDataProcessRequestDL(ProcReq pq, CompContext ct)
        {
            try
            {
                pq.OrderPlacedDate = DateTime.Now;
                ct.ProcessRequest.Add(pq);
                ct.SaveChanges();

                var testcred = ct.CredDetails;
                CreditDetail cd = testcred.Find(pq.CreditCardNumber);
                if (cd == null)
                {
                    CreditDetail obj = new CreditDetail();
                    obj.CreditcardNo = pq.CreditCardNumber;
                    obj.Creditlimit = 20000;
                    ct.CredDetails.Add(obj);
                    ct.SaveChanges();
                }
            }
            catch (Exception e) { }
        }

        public void AddDataProcessResponseDL(ProcRes pq, CompContext ct)
        {
            try
            {
                ct.ProcessResponse.Add(pq);
                ct.SaveChanges();
            }
            catch (Exception e) { }
        }

        public ProcRes Package_Delivery_DL(ProcReq value, CompContext ct)
        {
            HttpResponseMessage response = new HttpResponseMessage();
           
            string uriConn = URL.PackageDeliveryURL;
            string responseValue="";
           
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(uriConn);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    response = client.GetAsync(string.Format("api/PackagingDeliveryCharge?item={0}&count={1}", value.ComponentType, value.Quantity)).Result;

                }

                responseValue = response.Content.ReadAsStringAsync().Result;
                int y = Int32.Parse(responseValue);
            }
            catch (Exception e) {
                return null;
                //    _log4net.Error("Exception Occured" + e);
            }

            return PopulateProcRes(responseValue, value);

        }

        public int PaymentMicro_DL(ProcRes value, CompContext ct)
        {

            int pre_creditlimit = 0;
            int balanceamt = 0;
            HttpResponseMessage response = new HttpResponseMessage();
            
            string uriConn = URL.PaymentURL;
            try
            {

                var testcred = ct.CredDetails;
                CreditDetail cd = testcred.Find(value.CreditCardNo);
                if (cd != null)
                {
                    pre_creditlimit = cd.Creditlimit;
                }



                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(uriConn);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    response = client.GetAsync(string.Format("api/ProcessPayment?creditNo={0}&creditlimit={1}&totalcharge={2}", value.CreditCardNo, pre_creditlimit, value.TotalCharge)).Result;

                }
                string responseValue = response.Content.ReadAsStringAsync().Result;
                balanceamt = Int32.Parse(responseValue);


                if (cd != null)
                {
                    cd.Creditlimit = balanceamt;
                    ct.SaveChanges();
                }
            }

            catch (Exception e)
            {
                return -500;
                //    _log4net.Error("Exception Occured" + e);
            }

            return balanceamt;

        }

       
    }
}















//var response = await client.GetAsync( string.format("api/products/id={0}&type={1}",param.Id.Value,param.Id.Type) );
//http://localhost:5000/api/Auth?Username=123&Password=456
//  response = client.GetAsync("api/PackageDelivery/" + aadhar).Result;
//http://localhost:4697/api/PackagingDeliveryCharge?item=integral&count=0
// response = client.GetAsync(string.Format("api/PackageDelivery/componentname={0}&count={1}", value.ComponentName, value.Quantity)
// PackCharge t =  JsonConvert.DeserializeObject<PackCharge>(responseValue);

//    string responseValue = response.Content.ReadAsStringAsync().Result;
//  res = JsonConvert.DeserializeObject<ProcRes>(responseValue);
//http://localhost:57287/api/ProcessPayment?creditNo=111&creditlimit=11&totalcharge=12

/*
  Fetch value based on credit card No

  var anu = obj.Courses;
    Course st = anu.Find(id);
    if (st != null)
    {
        st.CourseName = cname;
        if (obj.SaveChanges() > 0)
        {
            Console.WriteLine("updated Successfully");
        }
    } 
 //  string uriConn = "http://localhost:4697/";
   //var response = await client.GetAsync( string.format("api/products/id={0}&type={1}",param.Id.Value,param.Id.Type) );
                    //http://localhost:5000/api/Auth?Username=123&Password=456
                    //  response = client.GetAsync("api/PackageDelivery/" + aadhar).Result;
                    //http://localhost:4697/api/PackagingDeliveryCharge?item=integral&count=0
                    // response = client.GetAsync(string.Format("api/PackageDelivery/componentname={0}&count={1}", value.ComponentName, value.Quantity)).Result;
 //  res.ProcessingCharge = 300*value.Quantity;

     int delivery_packaging_charges = Int32.Parse(responseValue);
            var random = new Random();
            res.RequestId = random.Next(9999, 99999);
            res.UserName = value.UserName;
            res.CreditCardNo = value.CreditCardNumber;
            res.PackagingAndDeliveryCharge = delivery_packaging_charges;
            if (value.IsPriorityRequest && value.ComponentName == "Repair")
            {
                res.ProcessingCharge = Price.PriorityRepair * value.Quantity;
              
                res.DateOfDelivery = value.OrderPlacedDate.AddDays(2);
            }
            else if (value.IsPriorityRequest == false && value.ComponentName == "Repair")
            {
                res.ProcessingCharge = Price.NoPriorityRepair * value.Quantity;
                
                res.DateOfDelivery = value.OrderPlacedDate.AddDays(5);
            }
            else {
                res.ProcessingCharge = Price.Replacement * value.Quantity;
            
                res.DateOfDelivery = value.OrderPlacedDate.AddDays(5);
            }
            res.TotalCharge = res.ProcessingCharge + res.PackagingAndDeliveryCharge;
            return res;


             HttpResponseMessage response = new HttpResponseMessage();
            string uriConn = "http://localhost:4697/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uriConn);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                
               
                   
               response = client.GetAsync(string.Format("api/PackagingDeliveryCharge?item={0}&count={1}", value.ComponentType, value.Quantity)).Result;
                
               
            }
            
            string responseValue = response.Content.ReadAsStringAsync().Result;

            return PopulateProcRes(responseValue,value);

 public int PaymentMicro_DL(ProcRes value, CompContext ct)
        {
            
            int pre_creditlimit = 0;
            var testcred = ct.CredDetails;
            CreditDetail cd = testcred.Find(value.CreditCardNo);
            if (cd != null) {
                pre_creditlimit = cd.Creditlimit;
            }

            HttpResponseMessage response = new HttpResponseMessage();
            string uriConn = "http://localhost:57287/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uriConn);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                 
                    response = client.GetAsync(string.Format("api/ProcessPayment?creditNo={0}&creditlimit={1}&totalcharge={2}", value.CreditCardNo, pre_creditlimit, value.TotalCharge)).Result;
                }
                catch (Exception e)
                {
                    //    _log4net.Error("Exception Occured" + e);
                
                }
            }
            string responseValue = response.Content.ReadAsStringAsync().Result;
            int balanceamt = Int32.Parse(responseValue);

            
            if (cd != null)
            {
                cd.Creditlimit = balanceamt;
                ct.SaveChanges();
            }

            return balanceamt;

        }
// string uriConn = "http://localhost:57287/";
 
            CreditDetail obj = new CreditDetail();
            obj.CreditcardNo = pq.CreditCardNumber;
            obj.Creditlimit  = 20000;
            ct.CredDetails.Add(obj);
            ct.SaveChanges();
            
            var testcred = ct.CredDetails;
            CreditDetail cd = testcred.Find(value.CreditCardNo);
            if (cd != null)
            {
                pre_creditlimit = cd.Creditlimit;
            }
            


*/
