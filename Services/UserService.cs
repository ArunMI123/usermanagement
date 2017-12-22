using System;
using System.Collections.Generic;
using System.Linq;
using ARBUserManagement.Repository;
using ARBUserManagement.Models;
using System.Collections.ObjectModel;

namespace ARBUserManagement.Services
{
    public class UserService : IUserService
    {
        private IUserRepository requestRepository;

        public UserService(IUserRepository _requestRepository)
        {
            requestRepository = _requestRepository;
        }
        

        public bool CheckDomainDuplicate(Domain domain)
        {
            // Check for Domain Duplicate Request
            try
            {
                bool isDuplicate = requestRepository.GetAllDomains()
                    .Where(x => x.DomainID != domain.DomainID
                    && x.DomainName == domain.DomainName
                    && x.Type == domain.Type
                ).Any();
                return isDuplicate;
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }


        public static string[] getUserDetails(string ticket)
        {
            string strToken = string.Empty;
            var base64EncodedBytes = System.Convert.FromBase64String(ticket);
            strToken = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            string[] auth = strToken.Split(':');
            return auth;
        }

        /// <summary>
        ///  Description: AddDomain Functionality.
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public int AddDomain(Domain domain)
        {
            try
            {
                int returnValue = requestRepository.AddDomain(domain);
                return returnValue;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public int saveUser(Reviewer reviewer)
        {
            int result;
            try
            {

                int reviewerId = reviewer.ReviewerID;
                if (reviewerId == 0)
                {
                    result = requestRepository.AddUser(reviewer);
                }
                else
                {
                    result = requestRepository.UpdateUser(reviewer);
                }
                /*
                var actionItem = reviewRepository.RetriveActionItem();
                result = actionItem.Where(x => x.RequestID == requestId);
                */

            }
            catch (Exception exception)
            {

                throw exception;
            }
            return result;
        }


        public bool CheckSaveUserDuplicate(Reviewer reviewer)
        {
            // Check for SaveUser Duplicate Request
            bool isDuplicate = true;
            try
            {

                isDuplicate = requestRepository.GetAllreviewer()
                   .Where(x =>
                   x.ReviewerID != reviewer.ReviewerID &&
                   x.ReviewerEmail == reviewer.ReviewerEmail

               ).Any();


                return isDuplicate;
            }


            catch (Exception exception)
            {
                throw exception;
            }

        }

        public Reviewer GetDomainUser(String email)
        {
            Reviewer result = requestRepository.GetUser(email);
            return result;
        }

        public IList<Reviewer> GetUserList()
        {
            List<Reviewer> userList = requestRepository.GetUserList().ToList();
            userList = userList.Select(x => new Reviewer
            {
                ReviewerID = x.ReviewerID,
                ReviewerName = x.ReviewerName,
                ReviewerEmail = x.ReviewerEmail,
                Role = x.Role,
                Active = x.Active,
                CreatedBy = x.CreatedBy,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate,
                ReviewerDomainAssignments = x.ReviewerDomainAssignments.Select(y => new ReviewerDomainAssignment { DomainID = y.DomainID, Domain = new Domain { DomainName = y.Domain.DomainName } }).ToList()
            }).ToList();
            return userList;
        }

        public Reviewer GetUser(int userID)
        {
            Reviewer result = requestRepository.GetUserList().Where(x => x.ReviewerID == userID).FirstOrDefault();
            return result;
        }

        /// <summary>
        /// Description:Function for retrive all domain .
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain> GetAllDomains()
        {
            IEnumerable<Domain> result = null;
            try
            {
                var domains = requestRepository.GetAllDomains();

                result = domains.AsEnumerable();

            }
            catch (Exception exception)
            {
                throw exception;
            }

            return result;

        }

        public int ReviewerAssignmentList(int reviewerID)
        {
            int result = requestRepository.RetriveReviewersAssigned().Where(x => x.ReviewerID == reviewerID).Count();
            return result;
        }
    }


}