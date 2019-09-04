using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectManagerAPI.Models;
using DAC = ProjectManagerAPI.Datalayer;

namespace ProjectManagerAPI.BusLayer
{

    public class TaskBL
    {
        DAC.ProjectManagerEntities1 dbcontext = null;

        public TaskBL()
        {
            dbcontext = new DAC.ProjectManagerEntities1();
        }
        public TaskBL(DAC.ProjectManagerEntities1 context)
        {
            dbcontext = context;
        }

        public List<Task> GetTaskByProjectId(int pid)
        {
            using (dbcontext)
            {
                return dbcontext.Tasks.Where(dataproject => dataproject.Project_ID == pid).Select(dataTask => new Task()
                {
                    TaskId = dataTask.Task_ID,
                    Task_Name = dataTask.Task_Name,
                    ParentTaskName = dbcontext.ParentTasks.Where(dataparenttasks => dataparenttasks.Parent_ID == dataTask.Parent_ID).FirstOrDefault().Parent_Task_Name,
                    Start_Date = dataTask.Start_Date,
                    End_Date = dataTask.End_Date,
                    Priority = dataTask.Priority,
                    Status = dataTask.Status,
                    User = dbcontext.Users.Where(datauserdata => datauserdata.Task_ID == dataTask.Task_ID).Select(userdata => new User()
                    {
                        UserId = userdata.User_ID,
                        FirstName = userdata.First_Name
                    }).FirstOrDefault(),
                }).ToList();
            }
        }

        public List<ParentTask> GetParentTasks()
        {
            using (dbcontext)
            {
                return dbcontext.ParentTasks.Select(x => new ParentTask()
                {
                    ParentTaskId = x.Parent_ID,
                    ParentTaskName = x.Parent_Task_Name
                }).ToList();
            }
        }
        public int InsertTaskDetails(Task task)
        {
            using (dbcontext)
            {

                if (task.Priority == 0)
                {
                    dbcontext.ParentTasks.Add(new DAC.ParentTask()
                    {
                        Parent_Task_Name = task.Task_Name

                    });
                }
                else
                {
                    DAC.Task taskDetail = new DAC.Task()
                    {
                        Task_Name = task.Task_Name,
                        Project_ID = task.Project_ID,
                        Start_Date = task.Start_Date,
                        End_Date = task.End_Date,
                        Parent_ID = task.Parent_ID,
                        Priority = task.Priority,
                        Status = task.Status
                    };
                    dbcontext.Tasks.Add(taskDetail);
                    dbcontext.SaveChanges();

                    var editDetails = (from editUser in dbcontext.Users
                                       where editUser.User_ID.ToString().Contains(task.User.UserId.ToString())
                                       select editUser).First();
                    // Modify existing records
                    if (editDetails != null)
                    {
                        editDetails.Task_ID = taskDetail.Task_ID;
                    }
                }
                return dbcontext.SaveChanges();
            }
        }

        public int UpdateTaskDetails(Task task)
        {
            using (dbcontext)
            {
                var editDetails = (from editTask in dbcontext.Tasks
                                   where editTask.Task_ID.ToString().Contains(task.TaskId.ToString())
                                   select editTask).First();
                // Modify existing records
                if (editDetails != null)
                {
                    editDetails.Task_Name = task.Task_Name;
                    editDetails.Start_Date = task.Start_Date;
                    editDetails.End_Date = task.End_Date;
                    editDetails.Status = task.Status;
                    editDetails.Priority = task.Priority;

                }
                var editDetailsUser = (from editUser in dbcontext.Users
                                       where editUser.User_ID.ToString().Contains(task.User.UserId.ToString())
                                       select editUser).First();
                // Modify existing records
                if (editDetailsUser != null)
                {
                    editDetails.Task_ID = task.TaskId;
                }
                return dbcontext.SaveChanges();
            }

        }

        public int DeleteTaskDetails(Task task)
        {
            using (dbcontext)
            {
                var deleteTask = (from editTask in dbcontext.Tasks
                                  where editTask.Task_ID.ToString().Contains(task.TaskId.ToString())
                                  select editTask).First();
                // Delete existing record
                if (deleteTask != null)
                {
                    deleteTask.Status = 1;
                }
                return dbcontext.SaveChanges();
            }

        }
    }
}