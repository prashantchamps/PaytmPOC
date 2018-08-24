using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PaytmCustomService.Controllers
{
    public class PaytmCustomApisController : ApiController
    {
        //public void Test()
        //{
        //    String value = "https://securegw-stage.paytm.in/refund/HANDLER_INTERNAL/REFUND?JsonData=";
        //    String Merchant_key = "I%VyKUMWdwEDyh4z";
        //    String MID = "PaytmS01829682567544";
        //    String order_id = "";
        //    String txnid_text = "";
        //    String ref_id = "";

        //    Dictionary innerrequest = new Dictionary();

        //    innerrequest.Add("MID", MID);
        //    innerrequest.Add("ORDERID", order_id);//withdraw order-id 
        //    innerrequest.Add("TXNTYPE", "REFUND");
        //    innerrequest.Add("REFUNDAMOUNT", Amount.Text);
        //    innerrequest.Add("TXNID", txnid_text);
        //    innerrequest.Add("REFID", ref_id);

        //    String first_jason = new JavaScriptSerializer().Serialize(innerrequest);

        //    try
        //    {
        //        string Check = paytm.CheckSum.generateCheckSumForRefund(Merchant_key, innerrequest);
        //        string correct_check = Server.UrlEncode(Check);

        //        innerrequest.Add("CHECKSUM", correct_check);
        //        String final = new JavaScriptSerializer().Serialize(innerrequest);
        //        final = final.Replace("\\", "").Replace(":\"{", ":{").Replace("}\",", "},");

        //        String url = value + final;

        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //        request.Headers.Add("ContentType", "application/json");
        //        request.Method = "POST";
        //        using (StreamWriter requestWriter2 = new StreamWriter(request.GetRequestStream()))
        //        {
        //            requestWriter2.Write(final);

        //        }
        //        string responseData = string.Empty;
        //        using (StreamReader responseReader = new StreamReader(request.GetResponse().GetResponseStream()))
        //        {
        //            responseData = responseReader.ReadToEnd();

        //            Response.Write("Requested Json= " + final);
        //            Response.Write("Response Json= " + responseData);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("Exception message: " + ex.Message.ToString());
        //    }
        //}

        [HttpGet]
        public void PlacePaytmOrder()
        {
            string orderId = "O" + CreateUniqueNumber();
            string merchantKey = "7jq7kPaSZKg1%9sV";
            var parameters = new Dictionary<string, string>();
            parameters.Add("MID", "StMary09045702657314");
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

            var request = (HttpWebRequest)WebRequest.Create(paytmURL);
            request.Method = "POST";

            HttpClient client = new HttpClient();
            var content = new FormUrlEncodedContent(parameters);
            var response = client.PutAsync(paytmURL, content);

        }

        private string CreateUniqueNumber()
        {
            string uniqueValue = string.Empty;
            uniqueValue = Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Second);
            return uniqueValue;
        }
    }
}
