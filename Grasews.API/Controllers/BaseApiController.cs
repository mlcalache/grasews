using AutoMapper;
using Grasews.Domain.Interfaces.Services;
using Newtonsoft.Json;
using System.Web.Http;

namespace Grasews.API.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public class BaseApiController : ApiController
    {
        /// <summary>
        ///
        /// </summary>
        protected readonly IMapper _mapper;

        /// <summary>
        ///
        /// </summary>
        protected readonly IUserIdentityService _userIdentityService;

        /// <summary>
        ///
        /// </summary>
        protected JsonSerializerSettings _jsonSerializerSettings;

        /// <summary>
        ///
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="userIdentityService"></param>
        public BaseApiController(IMapper mapper, IUserIdentityService userIdentityService)
        {
            _mapper = mapper;
            _userIdentityService = userIdentityService;
            _jsonSerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
        }
    }
}