using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;




namespace WebApplication1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            using (var httpClient = new HttpClient())
            {

                //get the question from the text box
                string questionstring = this.TextBox1.Text.ToString();

                // set the version number and Token for the api
                string version = "20160330";
                var accessToken = "6ITCGB4TENWFNIQVET63XTXEOYJFP3Q7";
                // create a guid to use for the session_Id (need to store this in the database)

                Guid myguid;
                myguid = Guid.NewGuid();

                //create the URI to call , include GUID, Version and Message
                string urlstring = string.Format("https://api.wit.ai/converse?v={0}&session_id={1}&q={2}", Server.UrlEncode(version),Server.UrlEncode(myguid.ToString()),Server.UrlEncode(questionstring));

                TextBox1.Text = urlstring.ToString();
                var url = new Uri(urlstring.ToString());
                
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                using (var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url))
                {

                    httpRequestMessage.Headers.Add(System.Net.HttpRequestHeader.Authorization.ToString(),
                      string.Format("Bearer {0}", accessToken));
                   // httpRequestMessage.Headers.Add("User-Agent", "My user-Agent");
                    using (var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage))
                    {
                        // do something with the response
                        var data = httpResponseMessage;
                        string strjson = data.Content.ReadAsStringAsync().Result;
                        dynamic jObj = (JObject)JsonConvert.DeserializeObject(strjson);

                        //http://weblogs.asp.net/sukumarraju/invoking-rest-web-service-deserializing-json-object-collection-to-anonymous-and-strongly-typed-objects


                        TextBox2.Text = strjson.ToString();



                        
                    }

                }
            }

        }
    }
}