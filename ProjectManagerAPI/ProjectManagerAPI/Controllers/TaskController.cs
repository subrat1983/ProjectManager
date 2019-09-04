using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectManagerAPI.Models;
using ProjectManagerAPI.BusLayer;


namespace ProjectManagerAPI.Controllers
{
    public class TaskController : ApiController
    {
        TaskBL taskObj = null;

        public TaskController()
        {
            taskObj = new TaskBL();
        }

        public TaskController(TaskBL taskBc)
        {
            taskObj = taskBc;
        }

        [HttpGet]
        [Route("api/task")]       
        public JsonResponse RetrieveTaskByProjectId(int projectId)
        {
            if (projectId < 0)
            {
                throw new ArithmeticException("ProjectId cannot be negative");
            }

            List<Task> Tasks = taskObj.GetTaskByProjectId(projectId);

            return new JsonResponse()
            {
                Data = Tasks
            };

        }

        [HttpGet]
        [Route("api/task/parent")]        
        public JsonResponse RetrieveParentTasks()
        {
            List<ParentTask> ParentTasks = taskObj.GetParentTasks();

            return new JsonResponse()
            {
                Data = ParentTasks
            };

        }

        [HttpPost]
        [Route("api/task/add")]
        public JsonResponse InsertTaskDetails(Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException("Task object is null");
            }
            if (task.Parent_ID < 0)
            {
                throw new ArithmeticException("Parent Id of task cannot be negative");
            }
            if (task.Project_ID < 0)
            {
                throw new ArithmeticException("Project Id cannot be negative");
            }
            if (task.TaskId < 0)
            {
                throw new ArithmeticException("Task id cannot be negative");
            }
            return new JsonResponse()
            {
                Data = taskObj.InsertTaskDetails(task)
            };

        }

        [HttpPost]        
        [Route("api/task/update")]
        public JsonResponse UpdateTaskDetails(Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException("Task object is null");
            }
            if (task.Parent_ID < 0)
            {
                throw new ArithmeticException("Parent Id of task cannot be negative");
            }
            if (task.Project_ID < 0)
            {
                throw new ArithmeticException("Project Id cannot be negative");
            }
            if (task.TaskId < 0)
            {
                throw new ArithmeticException("Task id cannot be negative");
            }
            return new JsonResponse()
            {
                Data = taskObj.UpdateTaskDetails(task)
            };

        }
        [HttpPost]        
        [Route("api/task/delete")]
        public JsonResponse DeleteTaskDetails(Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException("Task object is null");
            }
            if (task.Parent_ID < 0)
            {
                throw new ArithmeticException("Parent Id of task cannot be negative");
            }
            if (task.Project_ID < 0)
            {
                throw new ArithmeticException("Project Id cannot be negative");
            }
            if (task.TaskId < 0)
            {
                throw new ArithmeticException("Task id cannot be negative");
            }
            return new JsonResponse()
            {
                Data = taskObj.DeleteTaskDetails(task)
            };
        }

    }
}

