using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectManagerAPI.Datalayer;
using ProjectManagerAPI.BusLayer;
using MODEL = ProjectManagerAPI.Models;

namespace ProjectManagerAPI.BusLayer
{
    public class UserBL
    {

        ProjectManagerEntities1 dbcontext = null;
        public UserBL()
        {           
            dbcontext = new ProjectManagerAPI.Datalayer.ProjectManagerEntities1();
        }
        public UserBL(ProjectManagerAPI.Datalayer.ProjectManagerEntities1 context)
        {
            dbcontext = context;
        }

        public List<MODEL.User> GetUser()
        {
            using (dbcontext)
            {
                return dbcontext.Users.Select(data => new MODEL.User()
                {
                    FirstName = data.First_Name,
                    LastName = data.Last_Name,
                    EmployeeId = data.Employee_ID,
                    UserId = data.User_ID
                }).ToList();
            }
        }

        public int InsertUserDetails(MODEL.User user)
        {
            using (dbcontext)
            {
                dbcontext.Users.Add(new Datalayer.User()
                {
                    Last_Name = user.LastName,
                    First_Name = user.FirstName,
                    Employee_ID = user.EmployeeId
                });
                return dbcontext.SaveChanges();
            }
        }

        public int UpdateUserDetails(MODEL.User user)
        {
            using (dbcontext)
            {
                var editDetails = (from editUser in dbcontext.Users
                                   where editUser.User_ID == user.UserId
                                   select editUser).First();
               //if data exists update
                if (editDetails != null)
                {
                    editDetails.First_Name = user.FirstName;
                    editDetails.Last_Name = user.LastName;
                    editDetails.Employee_ID = user.EmployeeId;

                }
                return dbcontext.SaveChanges();
            }

        }

        public int DeleteUserDetails(MODEL.User user)
        {
            using (dbcontext)
            {
                var editDetails = (from editUser in dbcontext.Users
                                   where editUser.User_ID == user.UserId
                                   select editUser).First();
               
                if (editDetails != null)
                {
                    dbcontext.Users.Remove(editDetails);
                }
                return dbcontext.SaveChanges();
            }

        }
    }
}