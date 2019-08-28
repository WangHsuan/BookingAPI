using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIbooking.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string AccessToken { get; set; }
        public bool IsComplete { get; set; }

        public string client_id { get; set; }
        public string client_sercet { get; set; }

        public string resource { get; set; }

        public string grent_type { get; set; }

    }
}
