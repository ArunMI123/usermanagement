using System;
using System.Collections.Generic;

namespace ARBUserManagement.Models
{
    public partial class DmacceptComment
    {
        public int DmacceptCommentsId { get; set; }
        public int RequestId { get; set; }
        public int Phase { get; set; }
        public string Comments { get; set; }
        public int Acceptance { get; set; }

        public ReviewType AcceptanceNavigation { get; set; }
        public RequestForm Request { get; set; }
    }
}
