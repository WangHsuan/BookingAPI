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
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APIbooking.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/todo")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly TodoContext _context;

        //HttpWebResponse response;

        public ToDoController(TodoContext context) {

            
            _context = context;
            

            // Create a request using a URL that can receive a post.   
            WebRequest request = WebRequest.Create("https://login.microsoftonline.com/WangHsuan.onmicrosoft.com/oauth2/token");
            // Set the Method property of the request to POST.  
            request.Method = "POST";

            // Create POST data and convert it to a byte array.  
            string postData = "client_id=7d0a57c8-6f93-425d-9eb0-83a45a401277 & client_secret=bXW4wqCkIX4*2N3rCD*NMpwrttBKxy-.& grant_type=client_credentials & resource=https://graph.microsoft.com";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.  
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;

            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.  
            dataStream.Close();

            // Get the response.  
            WebResponse response = request.GetResponse();
            

            // Get the stream containing content returned by the server.  
            // The using block ensures the stream is automatically closed.
            using (dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.  
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.  
                string responseFromServer = reader.ReadToEnd();

                var hsuanJson = JObject.Parse(responseFromServer);
                // Display the content.  
                _context.TodoItems.Add(new TodoItem { AccessToken = hsuanJson["access_token"].ToString() });
                _context.SaveChanges();
                
            }

            // Close the response.  
            response.Close();


        }

        
        // GET: api/Todo
        [Microsoft.AspNetCore.Mvc.HttpGet]

        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            
            return await _context.TodoItems.ToListAsync();
        }



        //Post
        //[Microsoft.AspNetCore.Mvc.HttpPost]
        //public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item)
        //{
        //    _context.TodoItems.Add(item);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        //}

        
        // DELETE: api/Todo/5
        //[Microsoft.AspNetCore.Mvc.HttpDelete]
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