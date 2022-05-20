using ComponentProcessing.BL;
using ComponentProcessing.EntityModel;
using ComponentProcessing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentProcessing.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CompController : ControllerBase
    {
        private readonly CompContext _context;
        private readonly IBusinessL _bl;
        public CompController(CompContext context,IBusinessL bl)
        {
            _context = context;
            _bl = bl;
        }



        [HttpGet]
        public IActionResult ProcessDetail(string UserName, string ContactNumber, string CreditCardNumber, string ComponentType, string ComponentName, int Quantity, bool IsPriorityRequest) {
            ProcReq obj = new ProcReq();
            obj.UserName = UserName;
            obj.ContactNumber = ContactNumber;
            obj.CreditCardNumber = CreditCardNumber;
            obj.ComponentType = ComponentType;
            obj.ComponentName = ComponentName;
            obj.Quantity = Quantity;
            obj.IsPriorityRequest = IsPriorityRequest;
            // save process request
           // _bl.AddDataBL(obj, _context);
            ProcRes pr_resp_obj =  _bl.ProcessResponsefromPD_MicroBL(obj,_context);
            //save process response
            if (pr_resp_obj == null) { return BadRequest(); }
            // save process req and reponse , if its a valid request
            CreditDetail creditobj = _bl.AddDataBL(obj, _context);
            if (creditobj == null) { return BadRequest(); }
            _bl.AddDataProcessResponseBL(pr_resp_obj, _context);
            return Ok(pr_resp_obj);
            
        }


        [HttpPost]
        public IActionResult CompleteProcessing([FromBody] ProcRes value)
        {
            int balanceamt = _bl.PaymentBusiness(value, _context);
            if (balanceamt == -500)
            {
                return BadRequest();
            }
            return Ok(balanceamt);

        }

    }
}
