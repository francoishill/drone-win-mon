using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drone_win_mon.Drone
{
    public class BuildResponse
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Parent { get; set; }
        public string Event { get; set; }
        public string Status { get; set; }
        public string Error { get; set; }
        public int Enqueued_At { get; set; }
        public int Created_At { get; set; }
        public int Started_At { get; set; }
        public int Finished_At { get; set; }
        public string Deploy_To { get; set; }
        public string Commit { get; set; }
        public string Branch { get; set; }
        public string Ref { get; set; }
        public string Refspec { get; set; }
        public string Remote { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int Timestamp { get; set; }
        public string Author { get; set; }
        public string Author_Avatar { get; set; }
        public string Author_Email { get; set; }
        public string Link_Url { get; set; }
        public bool Signed { get; set; }
        public bool Verified { get; set; }
        public object[] Jobs { get; set; }
    }
}
