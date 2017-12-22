using ARBUserManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARBUserManagement.Repository
{
    public interface IUserRepository
    {
        IQueryable<Domain> GetAllDomains();
        int AddDomain(Domain domain);
        int AddUser(Reviewer reviewer);
        int UpdateUser(Reviewer reviewer);
        Reviewer GetUser(string email);
        IQueryable<Reviewer> GetAllreviewer();
        IQueryable<Reviewer> GetUserList();
        IQueryable<ReviewerAssignment> RetriveReviewersAssigned();
    }
}
