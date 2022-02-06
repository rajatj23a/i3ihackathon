using DecodingINS.Models;
using Newtonsoft.Json;
using SelectPdf;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace DecodingINS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CCDModel cCDModel)
        {

            //return View();
            return Content("hello");

        }

        [HttpPost]
        public ActionResult GenerateFocusGroup(string region, string country, string state)
        {
            var focusGroupList = new List<FocusGroup>();
            string json = "{\"type\": \"FocusGroup\" }";
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] data = encoder.GetBytes(json); // a json object, or xml, whatever...

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://fndecodeins.azurewebsites.net/api/DecodingFunction?code=B/K6DD6utnm99wCyqGcNqp2Mr6Kcb8W4OHpySyyIvtlHsBVaEVsSAQ==&type=focusgroup&resultTable=tblResultFocusGroup&area=" + state);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.Expect = "application/json";

            request.GetRequestStream().Write(data, 0, data.Length);

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                focusGroupList = JsonConvert.DeserializeObject<List<FocusGroup>>(result);
            }

            return PartialView("_FocusGroupView", focusGroupList);
        }


        [HttpPost]
        public ActionResult GenerateRealTimePremium(RealTimePremiumData realTimePremiumData)
        {
            var realTimePremiumDataList = new List<RealTimePremiumData>();

            string json = "{\"type\": \"RealTimePremium\" }";
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] data = encoder.GetBytes(json); // a json object, or xml, whatever...

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://fndecodeins.azurewebsites.net/api/DecodingFunction?code=B/K6DD6utnm99wCyqGcNqp2Mr6Kcb8W4OHpySyyIvtlHsBVaEVsSAQ==&type=realtimepremium&uid=397-78-800-0212&area=Delhi");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.Expect = "application/json";

            request.GetRequestStream().Write(data, 0, data.Length);

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                realTimePremiumDataList = JsonConvert.DeserializeObject<List<RealTimePremiumData>>(result);
            }

            return PartialView("_RealTimePremiumView", realTimePremiumDataList);
        }

        public void GenerateFocusGroupPDFReport(string data)
        {
            var myHtml = "<style>h1 {font-size:12px;}</style><h1>Test</h1><p style='font-weight:bold'>GenerateFocusGroupPDFReport</p>";

            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument doc = converter.ConvertHtmlString(myHtml);
            doc.Save("FocusGroupReport.pdf");
            doc.Close();
        }

        public void GenerateRealTimePremiumPDFReport(string data)
        {
            var myHtml = "<style>h1 {font-size:12px;}</style><h1>Test</h1><p style='font-weight:bold'>GenerateRealTimePremium</p>";

            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument doc = converter.ConvertHtmlString(myHtml);
            doc.Save("RealTimePremiumReport.pdf");
            doc.Close();
        }
    }
}