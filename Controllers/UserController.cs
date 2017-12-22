using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ARBUserManagement.Models;
using ARBUserManagement.Services;
using System.Linq;


namespace ARBUserManagement.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        IUserService userService;



        public UserController(IUserService _userService)
        {
            userService = _userService;

        }


        [HttpPost]
        [Route("SaveUser")]
        public IActionResult SaveUser([FromBody]Reviewer reviewer)
        {
            if (userService.CheckSaveUserDuplicate(reviewer))
            {
                return StatusCode(409);
            }
            int id = userService.saveUser(reviewer);
            Reviewer result = userService.GetUser(id);
            return Json(result);
        }

        /// <summary>
        /// Description:AddDomain Functionality. 
        /// </summary>
        /// <param name="doamin"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddDomain")]
        public IActionResult AddDomain([FromBody] Domain domain)
        {
            if (userService.CheckDomainDuplicate(domain))
            {
                return StatusCode(409);
            }
            int returnValue = userService.AddDomain(domain);
            var result = userService.GetAllDomains();
            return Json(result);
        }

        /// <summary>
        /// Description:Function for get all domains
        /// </summary>
        /// <returns>Domain map</returns>
        [Route("Domains")]
        public IActionResult GetDomains()
        {
            IEnumerable<Domain> result = userService.GetAllDomains();

            if (!result.Any())
            {
                return StatusCode(409);
            }

            return Json(result);
        }

        [HttpGet]
        [Route("UserList")]
        public IEnumerable<Reviewer> UserList()
        {
            IEnumerable<Reviewer> result = userService.GetUserList();

            return result;
        }

        [HttpGet]
        [Route("ReviwerAssignmentList/{reviewerID}")]
        public int ReviwerAssignmentList(int reviewerID)
        {
            int result = userService.ReviewerAssignmentList(reviewerID);


            return result;
        }



    }
}
