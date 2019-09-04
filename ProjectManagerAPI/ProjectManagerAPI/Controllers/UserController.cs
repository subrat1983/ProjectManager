using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectManagerAPI.BusLayer;
using DAC = ProjectManagerAPI.Datalayer;
using ProjectManagerAPI.Models;

namespace ProjectManagerAPI.Controllers
{
    public class UserController : ApiController
    {
        UserBL _userObjBC = null;

        public UserController()
        {
            _userObjBC = new UserBL();
        }

        public UserController(UserBL userObjBC)
        {
            _userObjBC = userObjBC;
        }

        [HttpGet]
        [Route("api/user")]
        public JsonResponse GetUser()
        {
            List<User> Users = _userObjBC.GetUser();

            return new JsonResponse()
            {
                Data = Users
            };
        }

        [HttpPost]
        [Route("api/user/add")]
        public JsonResponse InsertUserDetails(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User id is blank");
            }
            try
            {
                int employeeId = Convert.ToInt32(user.EmployeeId);
            }
            catch (FormatException ex)
            {
                throw new FormatException("Invalid employee Id format", ex);
            }
            if (Convert.ToInt32(user.EmployeeId) < 0)
            {
                throw new ArithmeticException("Negative value not allowed for employee id");
            }
            if (Convert.ToInt32(user.ProjectId) < 0)
            {
                throw new ArithmeticException("Negative value not allowed for Project id");
            }
            return new JsonResponse()
            {
                Data = _userObjBC.InsertUserDetails(user)
            };

        }

        [HttpPost]
        [Route("api/user/update")]
        public JsonResponse UpdateUserDetails(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User id is null");
            }
            try
            {
                int employeeId = Convert.ToInt32(user.EmployeeId);
            }
            catch (FormatException ex)
            {
                throw new FormatException("Invalid format of employee Id", ex);
            }
            if (Convert.ToInt32(user.EmployeeId) < 0)
            {
                throw new ArithmeticException("Employee id cannot be negative");
            }
            if (Convert.ToInt32(user.ProjectId) < 0)
            {
                throw new ArithmeticException("Project id cannot be negative");
            }
            if (user.UserId <= 0)
            {
                throw new ArithmeticException("User id cannot be negative or 0");
            }
            return new JsonResponse()
            {
                Data = _userObjBC.UpdateUserDetails(user)
            };
        }

        [HttpPost]
        [Route("api/user/delete")]
        public JsonResponse DeleteUserDetails(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User id is null");
            }
            try
            {
                int employeeId = Convert.ToInt32(user.EmployeeId);
            }
            catch (FormatException ex)
            {
                throw new FormatException("Invalid format of employee Id", ex);
            }
            if (Convert.ToInt32(user.EmployeeId) < 0)
            {
                throw new ArithmeticException("Employee id cannot be negative");
            }
            if (Convert.ToInt32(user.ProjectId) < 0)
            {
                throw new ArithmeticException("Project id cannot be negative");
            }
            if (user.UserId <= 0)
            {
                throw new ArithmeticException("User id cannot be negative or 0");
            }
            return new JsonResponse()
            {
                Data = _userObjBC.DeleteUserDetails(user)
            };
        }
    }
}
