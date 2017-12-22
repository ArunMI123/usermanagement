using ARBUserManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace ARBUserManagement.Services
{
    public interface IUserService
    {
        bool CheckDomainDuplicate(Domain domain);
        bool CheckSaveUserDuplicate(Reviewer reviewer);
        int saveUser(Reviewer reviewer);
        int AddDomain(Domain domain);
        IEnumerable<Domain> GetAllDomains();
        Reviewer GetDomainUser(String email);
        Reviewer GetUser(int userID);
        IList<Reviewer> GetUserList();
        int ReviewerAssignmentList(int reviewerID);

    }
}
