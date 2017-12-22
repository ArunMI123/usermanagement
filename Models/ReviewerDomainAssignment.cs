using System;
using System.Collections.Generic;

namespace ARBUserManagement.Models
{
    public partial class ReviewerDomainAssignment
    {
        public int ReviewerID { get; set; }
        public int DomainID { get; set; }
        public int DomainAssignmentId { get; set; }

        public Domain Domain { get; set; }
        public Reviewer Reviewer { get; set; }
    }
}
