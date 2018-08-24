using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using paytm;

namespace PaytmApiPoc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin1()
        {
            return View("Admin1");
        }

        public ActionResult Admin2()
        {
            return View("Admin2");
        }

        public ActionResult Parent1()
        {
            return View("Parent1");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Paytm()
        {
            return View();
        }
        
        //[HttpPost]
        public ActionResult CreatePayment()
        {
            string orderId = "O" + CreateUniqueNumber();
            string merchantKey = Key.merchantKey;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("MID", Key.merchantId);
            parameters.Add("REQUEST_TYPE", "DEFAULT");
            parameters.Add("CHANNEL_ID", "WEB");
            parameters.Add("INDUSTRY_TYPE_ID", "Retail");
            parameters.Add("WEBSITE", "WEBSTAGING");
            parameters.Add("EMAIL", "prashant.champs8@gmail.com");
            parameters.Add("MOBILE_NO", "9168894422");            
            parameters.Add("CALLBACK_URL", "http://localhost:60663/Home/PaytmResponse");
            parameters.Add("CUST_ID", "1");
            parameters.Add("ORDER_ID", orderId);
            parameters.Add("TXN_AMOUNT", "1000");
            string checksum = paytm.CheckSum.generateCheckSum(merchantKey, parameters);
            string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + orderId;

            string outputHTML = "<html>";
            outputHTML += "<head>";
            outputHTML += "<title>Merchant Check Out Page</title>";
            outputHTML += "</head>";
            outputHTML += "<body>";
            outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
            outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";
            
            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "' />";
            }

            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "' />";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</body>";
            outputHTML += "</html>";

            ViewBag.htmlData = outputHTML;
            return View("PaymentPage");
        }


        //public ActionResult CreatePayment()
        //{
        //    return View();
        //}

        private string CreateUniqueNumber()
        {
            string uniqueValue = string.Empty;
            uniqueValue = Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Second);
            return uniqueValue;
        }

        [HttpPost]
        public ActionResult paytmResponse(Models.PaytmResponse response)
        {
            return View("paytmResponse", response);
        }
    }
}