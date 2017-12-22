using ARBUserManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ARBUserManagement.Repository
{
    public class UserRepository : IUserRepository
    {
        private ARB_DevelopContext dbcontext;

        public UserRepository(ARB_DevelopContext _context)
        {
            dbcontext = _context;

        }

        public IQueryable<Reviewer> GetUserList()
        {
            IQueryable<Reviewer> reviewersQuery = from reviewers in dbcontext.Reviewers.Include("ReviewerDomainAssignments.Domain")

                                                  select reviewers;
            return reviewersQuery;
        }
        /// <summary>
        ///check for Reviewer Duplicates 
        /// </summary>
        /// <returns></returns>
        public IQueryable<Reviewer> GetAllreviewer()
        {
            IQueryable<Reviewer> result = null;

            result = from reviewer in dbcontext.Reviewers
                     select reviewer;

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<RequestForm> GetAllRequestForms()
        {
            IQueryable<RequestForm> result = null;

            result = from requestForms in dbcontext.RequestForms.Include("QuestionResponses")
                     select requestForms;

            return result;
        }

        /// <summary>
        ///check for Domain Duplicates 
        /// </summary>
        /// <returns></returns>
        public IQueryable<Domain> GetAllDomains()
        {
            IQueryable<Domain> result = null;

            result = from domain in dbcontext.Domains
                     select domain;

            return result;
        }

              


        public int AddUser(Reviewer reviewer)
        {
            dbcontext.Reviewers.Add(reviewer);

            dbcontext.SaveChanges();
            return reviewer.ReviewerID;
        }

        public int UpdateUser(Reviewer reviewer)
        {
            Reviewer _user = dbcontext.Reviewers.Where(p => p.ReviewerID == reviewer.ReviewerID).Include(p => p.ReviewerDomainAssignments).FirstOrDefault();
            dbcontext.Entry(_user).CurrentValues.SetValues(reviewer);

            foreach (var ReviewerDomain in _user.ReviewerDomainAssignments.ToList())
            {
                if (!reviewer.ReviewerDomainAssignments.Any(c => c.DomainID == ReviewerDomain.DomainID))
                {
                    dbcontext.ReviewerDomainAssignments.Remove(ReviewerDomain);
                }
            }
            foreach (var newDomainAssignment in reviewer.ReviewerDomainAssignments)
            {
                var domainAssign = _user.ReviewerDomainAssignments
                    .Where(c => c.DomainID == newDomainAssignment.DomainID)
                    .SingleOrDefault();

                if (domainAssign == null)
                {

                    _user.ReviewerDomainAssignments.Add(newDomainAssignment);
                }


            }
            dbcontext.SaveChanges();
            return reviewer.ReviewerID;
        }


        /// <summary>
        /// Description:AddDomain Functionality.
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public int AddDomain(Domain domain)
        {
            int returnValue = -1;
            try
            {
                if (domain != null)
                {
                    dbcontext.Domains.Add(domain);
                    int x = dbcontext.SaveChanges();
                    if (x > 0)
                    {
                        returnValue = 1;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return returnValue;
        }

        public Reviewer GetUser(string email)
        {
            Reviewer result = dbcontext.Reviewers.Where(x => x.ReviewerEmail == email).FirstOrDefault();
            return result;
        }

        /// <summary>
        /// Description:function to retrive requester to request in db
        /// </summary>
        /// <returns></returns>
        public IQueryable<ReviewerAssignment> RetriveReviewersAssigned()
        {
            IQueryable<ReviewerAssignment> result = null;

            result = from reviewersAssigned in dbcontext.ReviewerAssignments.Include("Reviewer").Include("Domain")
                     select reviewersAssigned;

            return result;
        }
    }
}