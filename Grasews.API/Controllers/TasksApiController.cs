using AutoMapper;
using Grasews.API.Models;
using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Grasews.API.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [RoutePrefix("api/tasks")]
    [DisplayName("Tasks")]
    public class TasksApiController : BaseApiController
    {
        #region Private vars

        /// <summary>
        ///
        /// </summary>
        private readonly ITaskService _taskService;

        /// <summary>
        ///
        /// </summary>
        private readonly ITaskCommentService _taskCommentService;

        #endregion Private vars

        #region Ctors

        /// <summary>
        ///
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="taskService"></param>
        /// <param name="taskCommentService"></param>
        /// <param name="userIdentityService"></param>
        public TasksApiController(IMapper mapper,
            ITaskService taskService,
            ITaskCommentService taskCommentService,
            IUserIdentityService userIdentityService)
            : base(mapper, userIdentityService)
        {
            _taskService = taskService;
            _taskCommentService = taskCommentService;
        }

        #endregion Ctors

        #region Actions

        #region CRUD

        // GET: api/tasks
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IEnumerable<Task_ApiResponseViewModel>))]
        public IHttpActionResult GetTasks()
        {
            var tasks = _taskService.GetAll();

            if (tasks.Any())
            {
                var tasksResponseModel = _mapper.Map<List<Task_ApiResponseViewModel>>(tasks);

                return Ok(tasksResponseModel);
            }

            return NotFound();
        }

        // GET: api/tasks/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(Task_ApiResponseViewModel))]
        public IHttpActionResult GetTask(int id)
        {
            var task = _taskService.Get(id);

            if (task == null)
            {
                return NotFound();
            }

            var taskResponseModel = _mapper.Map<Task_ApiResponseViewModel>(task);

            return Ok(taskResponseModel);
        }

        // PUT: api/tasks/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="taskRequestUpdateModel"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(Task_ApiResponseViewModel))]
        public IHttpActionResult PutTask(int id, Task_ApiRequestUpdateModel taskRequestUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id <= 0)
            {
                return BadRequest("Invalid Id.");
            }

            var existingTask = _taskService.Get(id);

            if (existingTask == null)
            {
                return NotFound();
            }

            var idUser = _userIdentityService.Id;

            if (existingTask.IdOwnerUser != idUser)
            {
                return Unauthorized();
            }

            var task = _mapper.Map<Task>(taskRequestUpdateModel);

            task.Id = id;

            var count = _taskService.Update(task);

            var taskResponseModel = _mapper.Map<Task_ApiResponseViewModel>(task);

            return Ok(taskResponseModel);
        }

        // POST: api/tasks
        /// <summary>
        ///
        /// </summary>
        /// <param name="taskRequestCreateModel"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Task_ApiResponseViewModel))]
        public IHttpActionResult PostTask(Task_ApiRequestCreateModel taskRequestCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var idUser = _userIdentityService.Id;

            var task = _mapper.Map<Task>(taskRequestCreateModel);

            task.IdOwnerUser = idUser;

            var count = _taskService.Create(task);

            var taskResponseModel = _mapper.Map<Task_ApiResponseViewModel>(task);

            return Ok(taskResponseModel);
        }

        // DELETE: api/tasks/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(Task_ApiResponseViewModel))]
        public IHttpActionResult DeleteTask(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Id.");
            }

            var existingTask = _taskService.Get(id);

            if (existingTask == null)
            {
                return NotFound();
            }

            var idUser = _userIdentityService.Id;

            if (existingTask.IdOwnerUser != idUser)
            {
                return Unauthorized();
            }

            var count = _taskService.Remove(existingTask);

            var taskResponseModel = _mapper.Map<Task_ApiResponseViewModel>(existingTask);

            return Ok(taskResponseModel);
        }

        #endregion CRUD

        #region Comments

        // GET: api/tasks/1/comments
        /// <summary>
        /// Retrieves a list of Comments according to the Task
        /// </summary>
        /// <param name="idTask">todo: describe idTask parameter on GetTaskComments</param>
        /// <returns></returns>
        [Route("{idTask:int}/comments")]
        [ResponseType(typeof(IEnumerable<TaskComment_ApiResponseViewModel>))]
        public IHttpActionResult GetTaskComments(int idTask)
        {
            var taskComments = _taskCommentService.GetAll(idTask);

            if (taskComments.Any())
            {
                var taskCommentsResponseModel = _mapper.Map<List<TaskComment_ApiResponseViewModel>>(taskComments);

                return Ok(taskCommentsResponseModel);
            }

            return NotFound();
        }

        // GET: api/tasks/1/comments/5
        /// <summary>
        /// Retrieves an Comment according to its Id and the Todo Task
        /// </summary>
        /// <param name="idTask"></param>
        /// <param name="id">The Id of the Todo Task Comment</param>
        /// <returns></returns>
        [Route("{idTask:int}/comments/{id:int}")]
        [ResponseType(typeof(TaskComment_ApiResponseViewModel))]
        public IHttpActionResult GetTaskComment(int idTask, int id)
        {
            var taskComment = _taskCommentService.Get(idTask, id);

            if (taskComment == null)
            {
                return NotFound();
            }

            var taskCommentResponseModel = _mapper.Map<TaskComment_ApiResponseViewModel>(taskComment);

            return Ok(taskCommentResponseModel);
        }

        // PUT: api/tasks/1/comments/5
        /// <summary>
        /// Updates an Comment from an Todo Task
        /// </summary>
        /// <param name="idTask"></param>
        /// <param name="id">The Id of the Todo Task Comment</param>
        /// <param name="taskCommentRequestModel">The Todo Task Comment object with update values</param>
        /// <returns></returns>
        [Route("{idTask:int}/comments/{id:int}")]
        [ResponseType(typeof(TaskComment_ApiResponseViewModel))]
        public IHttpActionResult PutTaskComment(int idTask, int id, TaskComment_ApiRequestUpdateModel taskCommentRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != taskCommentRequestModel.Id)
            {
                return BadRequest();
            }

            var taskComment = _taskCommentService.Get(idTask, id);

            if (taskComment == null)
            {
                return NotFound();
            }

            var idUser = _userIdentityService.Id;

            if (taskComment.IdOwnerUser != idUser)
            {
                return Unauthorized();
            }

            taskComment = _mapper.Map<TaskComment>(taskCommentRequestModel);

            taskComment.IdTask = idTask;

            var count = _taskCommentService.Update(taskComment);

            var taskCommentResponseModel = _mapper.Map<TaskComment_ApiResponseViewModel>(taskComment);

            return Ok(taskCommentResponseModel);
        }

        // POST: api/tasks/1/comments/5
        /// <summary>
        /// Creates an Comment for an Todo Task
        /// </summary>
        /// <param name="idTask"></param>
        /// <param name="taskCommentRequestModel">The Todo Task Comment object to be created</param>
        /// <returns></returns>
        [Route("{idTask:int}/comments")]
        [ResponseType(typeof(TaskComment_ApiResponseViewModel))]
        public IHttpActionResult PostTaskComment(int idTask, TaskComment_ApiRequestCreateModel taskCommentRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var idUser = _userIdentityService.Id;

            var taskComment = _mapper.Map<TaskComment>(taskCommentRequestModel);

            taskComment.IdOwnerUser = idUser;
            taskComment.IdTask = idTask;

            var count = _taskCommentService.Create(taskComment);

            var taskCommentResponseModel = _mapper.Map<TaskComment_ApiResponseViewModel>(taskComment);

            return Ok(taskCommentResponseModel);
        }

        // DELETE: api/tasks/1/comments/5
        /// <summary>
        /// Deletes an Comment from an Todo Task
        /// </summary>
        /// <param name="idTask"></param>
        /// <param name="id">The Id of the Todo Task Comment</param>
        /// <returns></returns>
        [Route("{idTask:int}/comments/{id:int}")]
        [ResponseType(typeof(TaskComment_ApiResponseViewModel))]
        public IHttpActionResult DeleteTaskComment(int idTask, int id)
        {
            var taskComment = _taskCommentService.Get(idTask, id);

            if (taskComment == null)
            {
                return NotFound();
            }

            var idUser = _userIdentityService.Id;

            if (taskComment.IdOwnerUser != idUser)
            {
                return Unauthorized();
            }

            var count = _taskCommentService.Remove(taskComment);

            var taskCommentResponseModel = _mapper.Map<TaskComment_ApiResponseViewModel>(taskComment);

            return Ok(taskCommentResponseModel);
        }

        #endregion Comments

        #endregion Actions
    }
}