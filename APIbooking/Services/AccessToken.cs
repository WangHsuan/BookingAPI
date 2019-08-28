using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Web.Http.Cors;

namespace APIbooking.Services
{
    public class AccessToken
    {
        [EnableCors(origins: "http://localhost:61159", headers: "*", methods: "*")]
        public string GetAccessToken()
        {

            //建立 HttpClient
            HttpClient client = new HttpClient();
            // 將 data 轉為 json
            string _client_id = "7d0a57c8-6f93-425d-9eb0-83a45a401277";
            string _client_secert = "pgvJoQkjMGp+5Ti4Ny_l*rx+P7NqMgW7";
            string body = $"client_id={_client_id} & client_secret={_client_secert} & grant_type = client_credentials & resource = https://graph.microsoft.com/ ";

            // 將轉為 string 的 json 依編碼並指定 content type 存為 httpcontent
            HttpContent contentPost = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");
            // 發出 post 並取得結果
            HttpResponseMessage response = client.PostAsync($"https://login.microsoftonline.com/WangHsuan.onmicrosoft.com/oauth2/v2.0/token", contentPost).GetAwaiter().GetResult();

        
            return response.ToString();

           

        }
    }
}
