using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using InstagramAPI.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.IO;

namespace InstagramAPI.Controllers
{
    public class HomeController : Controller
    {
        WebClient client = new WebClient();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginInstagram()
        {
            var client_id = ConfigurationManager.AppSettings["InstaClientID"].ToString();
            var redirect_uri = ConfigurationManager.AppSettings["InstaRedirectURI"].ToString();
            Response.Redirect("https://api.instagram.com/oauth/authorize/?client_id=" + client_id + "&redirect_uri=" + redirect_uri + "&response_type=code");
            return View();
        }

        public ActionResult ManageFollowers()
        {
            //store code from query string
            var code = HttpContext.Request.QueryString["code"];

            //create parameters to POST to the URL
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("client_id", ConfigurationManager.AppSettings["InstaClientID"].ToString());
            parameters.Add("client_secret", ConfigurationManager.AppSettings["InstaClientSecret"].ToString());
            parameters.Add("grant_type", "authorization_code");
            parameters.Add("redirect_uri", ConfigurationManager.AppSettings["InstaRedirectURI"].ToString());
            parameters.Add("code", code);
          
            //POST the parameters to the URL
            var resultAccessToken = client.UploadValues("https://api.instagram.com/oauth/access_token", "POST", parameters);
            var responseAccessToken = System.Text.Encoding.Default.GetString(resultAccessToken);
            var UserBasicObject = JsonConvert.DeserializeObject<BasicInfo>(responseAccessToken);
            return View(UserBasicObject);
        }

        public void LoginFacebook()
        {
            var client_id = ConfigurationManager.AppSettings["FacebookClientID"].ToString();
            var redirect_uri = ConfigurationManager.AppSettings["FacebookRedirectURI"].ToString();
            Response.Redirect("https://www.facebook.com/v2.8/dialog/oauth?client_id=" + client_id + "&redirect_uri=" + redirect_uri + "&&response_type=code");
        }

        public ActionResult ManageFacebook()
        {
            //store code from query string
            var code = HttpContext.Request.QueryString["code"];

            //create parameters to POST to the URL
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("client_id", ConfigurationManager.AppSettings["FacebookClientID"].ToString());
            parameters.Add("client_secret", ConfigurationManager.AppSettings["FacebookClientSecret"].ToString());
            parameters.Add("redirect_uri", ConfigurationManager.AppSettings["FacebookRedirectURI"].ToString());
            parameters.Add("code", code);

            //GET parameters to the URL
            var resultAccessToken = client.UploadValues("https://graph.facebook.com/v2.8/oauth/access_token?", "POST", parameters);
            var responseAccessToken = System.Text.Encoding.Default.GetString(resultAccessToken);
            JObject j = JObject.Parse(responseAccessToken);
            JToken atoken = j["access_token"];

            Uri userUri = new Uri("https://graph.facebook.com/me?fields=first_name,last_name,picture&access_token=" + atoken.ToString());
            HttpWebRequest userInfo = (HttpWebRequest)HttpWebRequest.Create(userUri);

            StreamReader streamUserInfo = new StreamReader(userInfo.GetResponse().GetResponseStream());
            string jsonResponse = string.Empty;
            jsonResponse = streamUserInfo.ReadToEnd();
            var UserFacebookObject = JsonConvert.DeserializeObject<FacebookInfo>(jsonResponse);
            return View(UserFacebookObject);
        }
    }
}