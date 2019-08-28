using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIbooking.Models;
using Microsoft.EntityFrameworkCore;


using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;




using APIbooking.Services;
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace APIbooking.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/todo")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly TodoContext _context;

        //HttpWebResponse response;

        //[EnableCors(origins: "http://localhost:61159", headers: "*", methods: "*")]
        public static HttpResponseMessage getAccessToken() {

            //first bad request----------------------------------------------------
            HttpClient client = new HttpClient();

            // 將 data 轉為 json

            string body = "client_id=7d0a57c8-6f93-425d-9eb0-83a45a401277&client_secret=pgvJoQkjMGp+5Ti4Ny_l*rx+P7NqMgW7&grant_type=client_credentials&resource=https://graph.microsoft.com";

            // 將轉為 string 的 json 依編碼並指定 content type 存為 httpcontent
            HttpContent contentPost = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");
            // 發出 post 並取得結果
            HttpResponseMessage response = client.PostAsync("https://login.microsoftonline.com/WangHsuan.onmicrosoft.com/oauth2/token", contentPost).GetAwaiter().GetResult();

            return response;

            //Second Method---------------------------------------------------------------------------

            //string targetUrl = "https://login.microsoftonline.com/WangHsuan.onmicrosoft.com/oauth2/v2.0/token";
            //string _client_id = "7d0a57c8-6f93-425d-9eb0-83a45a401277";
            //string _client_secert = "pgvJoQkjMGp+5Ti4Ny_l*rx+P7NqMgW7";
            //string body = $"client_id={_client_id} & client_secret={_client_secert} & grant_type = client_credentials & resource = https://graph.microsoft.com ";
            //byte[] postData = Encoding.UTF8.GetBytes(body);

            //HttpWebRequest request = WebRequest.Create(targetUrl) as HttpWebRequest;
            //request.Method = "POST";
            //request.ContentType = "multipart/form-data";
            //request.Timeout = 30000;
            //request.ContentLength = postData.Length;
            //// 寫入 Post Body Message 資料流
            //using (Stream st = request.GetRequestStream())
            //{
            //    st.Write(postData, 0, postData.Length);
            //}

            //string result = "";
            //// 取得回應資料
            //using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            //{
            //    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            //    {
            //        result = sr.ReadToEnd();
            //    }
            //}

            //return result;

            //Third-----------------------------------------------------


            //// Create a request using a URL that can receive a post.   
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://login.microsoftonline.com/WangHsuan.onmicrosoft.com/oauth2/v2.0/token");
            //// Set the Method property of the request to POST.  
            //request.Method = "POST";

            //// Create POST data and convert it to a byte array. 
            //string _client_id = "7d0a57c8-6f93-425d-9eb0-83a45a401277";
            //string _client_secert = "pgvJoQkjMGp+5Ti4Ny_l*rx+P7NqMgW7";
            //string postData = $@"client_id={_client_id}&client_secret={_client_secert}&grant_type=client_credentials&scope=https://graph.microsoft.com/.default";
            //byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            //// Set the ContentType property of the WebRequest.  
            //request.ContentType = "application/x-www-form-urlencoded";
            //// Set the ContentLength property of the WebRequest.  
            //request.ContentLength = byteArray.Length;

            //// Get the request stream.  
            //Stream dataStream = request.GetRequestStream();
            //// Write the data to the request stream.  
            //dataStream.Write(byteArray, 0, byteArray.Length);
            //// Close the Stream object.  
            //dataStream.Close();

            //// Get the response.  

            //WebResponse response = request.GetResponse();


            ////catch (WebException e)
            ////{
            ////    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
            ////    else return false;
            ////}
            ////catch (Exception)
            ////{
            ////    if (response != null) response.Close();
            ////    return false;
            ////}
            //return response;

            //fourth---------------------------------------------------------



        }

        public ToDoController(TodoContext context) {

            
            _context = context;
            if (_context.TodoItems.Count() == 0) {
                _context.TodoItems.Add(new TodoItem { AccessToken = getAccessToken().ToString()});
                _context.SaveChanges();
            }

            
        }

        
        // GET: api/Todo
        [Microsoft.AspNetCore.Mvc.HttpGet]

        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            
            return await _context.TodoItems.ToListAsync();
        }

        //// GET: api/Todo/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        //{
        //    var todoItem = await _context.TodoItems.FindAsync(id);

        //    if (todoItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return todoItem;
        //}

        //Post
        //[System.Web.Http.HttpPost]
        //public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item) {
        //    _context.TodoItems.Add(item);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        //}

        //// PUT: api/Todo/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTodoItem(long id, TodoItem item)
        //{
        //    if (id != item.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(item).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //// DELETE: api/Todo/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTodoItem(long id)
        //{
        //    var todoItem = await _context.TodoItems.FindAsync(id);

        //    if (todoItem == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TodoItems.Remove(todoItem);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}