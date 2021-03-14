using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PayUmoneytry.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Demo()
        {
            return View();
        }


        [HttpPost]
        public void Demo(FormCollection form)
        {
            try
            {
                string firstName = form["txtfirstname"].ToString();
                string amount = form["txtamount"].ToString();
                string productInfo = form["txtprodinfo"].ToString();
                string email = form["txtemail"].ToString();
                string phone = form["txtphone"].ToString();
                string surl = "	https://pmny.in/GIoKBBCqWVep";  //Change the success url here depending upon the port number of your local system.
                string furl = "https://www.payumoney.com/mobileapp/payumoney/failure.php";  //Change the failure url here depending upon the port number of your local system.




                RemotePost myremotepost = new RemotePost();
                //Add your MarchantID;  
                string key = "uuG5oH";
                //Add your SaltID;  
                string salt = "z888erKQ";


                //posting all the parameters required for integration.  

                myremotepost.Url = "https://sandboxsecure.payu.in/_payment";
                myremotepost.Add("key", "4ptctt6n");
                string txnid = Generatetxnid();
                myremotepost.Add("txnid", txnid);
                myremotepost.Add("amount", amount);
                myremotepost.Add("productinfo", productInfo);
                myremotepost.Add("firstname", firstName);
                myremotepost.Add("phone", phone);
                myremotepost.Add("email", email);
                myremotepost.Add("surl", "https://pmny.in/GIoKBBCqWVep");//Change the success url here depending upon the port number of your local system.  
                myremotepost.Add("furl", "https://www.payumoney.com/mobileapp/payumoney/failure.php");//Change the failure url here depending upon the port number of your local system.  
                myremotepost.Add("service_provider", "payu_paisa");
                string hashString = key + "|" + txnid + "|" + amount + "|" + productInfo + "|" + firstName + "|" + email + "|||||||||||" + salt;
                string hash = Generatehash512(hashString);
                myremotepost.Add("hash", hash);

                myremotepost.Post();
            }
            catch (Exception exp)
            {
                throw;
            }


        }

        private string Generatetxnid()
        {
            Random rnd = new Random();
            string strHash = Generatehash512(rnd.ToString() + DateTime.Now);
            string txnid1 = strHash.ToString().Substring(0, 20);

            return txnid1;
        }

        private string Generatehash512(string text)
        {
            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }


        public class RemotePost
        {
            private System.Collections.Specialized.NameValueCollection Inputs = new System.Collections.Specialized.NameValueCollection();


            public string Url = "";
            public string Method = "post";
            public string FormName = "form1";

            public void Add(string name, string value)
            {
                Inputs.Add(name, value);
            }

            public void Post()
            {
                System.Web.HttpContext.Current.Response.Clear();

                System.Web.HttpContext.Current.Response.Write("<html><head>");

                System.Web.HttpContext.Current.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
                System.Web.HttpContext.Current.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
                for (int i = 0; i < Inputs.Keys.Count; i++)
                {
                    System.Web.HttpContext.Current.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", Inputs.Keys[i], Inputs[Inputs.Keys[i]]));
                }
                System.Web.HttpContext.Current.Response.Write("</form>");
                System.Web.HttpContext.Current.Response.Write("</body></html>");

                System.Web.HttpContext.Current.Response.End();
            }
        }
    }
}