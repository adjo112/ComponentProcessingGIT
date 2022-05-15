using Moq;
using NUnit.Framework;
using ComponentProcessing;
using ComponentProcessing.DL;
using ComponentProcessing.BL;
using ComponentProcessing.Models;
using ComponentProcessing.EntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ComponentProcessingTests
{
    public class BusinessLayerTests
    {
       
        [SetUp]
        public void Setup()
        {
                  
           

        }

        [Test]
        public void AddDataBL_Test()
        {
            
            ProcReq pq = new ProcReq();
            DbContextOptions<CompContext> Options = new DbContextOptions<CompContext>();
            CompContext cc = new CompContext(Options);
            Mock<IData> mockDataLayer = new Mock<IData>();
            BusinessL business_obj = new BusinessL(mockDataLayer.Object);
            business_obj.AddDataBL(pq, cc);
            Assert.Pass();   
        }

        [Test]
        public void AddDataProcessResponseBL_Test() {
            ProcRes res = new ProcRes();
            DbContextOptions<CompContext> Options = new DbContextOptions<CompContext>();
            CompContext cc = new CompContext(Options);
            Mock<IData> mockDataLayer = new Mock<IData>();
            BusinessL business_obj = new BusinessL(mockDataLayer.Object);
            business_obj.AddDataProcessResponseBL(res, cc);
            Assert.Pass();

        }

        [Test]
        public void PaymentBusiness_Test() {
            int preloaded_amt = 100;
            ProcRes res = new ProcRes();
            DbContextOptions<CompContext> Options = new DbContextOptions<CompContext>();
            CompContext cc = new CompContext(Options);
            Mock<IData> mockDataLayer = new Mock<IData>();
            mockDataLayer.Setup(q => q.PaymentMicro_DL(res, cc)).Returns(preloaded_amt);
            BusinessL business_obj = new BusinessL(mockDataLayer.Object);
            int balance_amt = business_obj.PaymentBusiness(res, cc);
            Assert.AreEqual(preloaded_amt, balance_amt);
        }

        [Test]
        public void ProcessResponsefromPD_MicroBL_Test() {
            ProcRes res = new ProcRes();
            res.RequestId = 1;
            res.UserName = "Ad";
            res.ProcessingCharge = 500;
            res.PackagingAndDeliveryCharge = 100;
            res.DateOfDelivery = System.DateTime.Now.AddDays(2);
            res.TotalCharge = 600;
            res.CreditCardNo = "122222222";


            ProcReq pq = new ProcReq();
            DbContextOptions<CompContext> Options = new DbContextOptions<CompContext>();
            CompContext cc = new CompContext(Options);
            Mock<IData> mockDataLayer = new Mock<IData>();
            mockDataLayer.Setup(x => x.Package_Delivery_DL(pq, cc)).Returns(res);
            BusinessL business_obj = new BusinessL(mockDataLayer.Object);

            ProcRes res_ret = business_obj.ProcessResponsefromPD_MicroBL(pq,cc);
            //Assert.IsNull(res);
            Assert.IsNotNull(res_ret);
            Assert.IsInstanceOf<ProcRes>(res_ret);
        }



    }
}

/*
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

RequestId UserName ProcessingCharge PackagingAndDeliveryCharge DateOfDelivery TotalCharge CreditCardNo
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

 */