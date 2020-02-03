using Microsoft.Owin;
using System;
using System.Threading.Tasks;

namespace Grasews.API.Providers
{
    /// <summary>
    ///
    /// </summary>
    public class ResponseStatusCodeMiddleware : OwinMiddleware
    {
        /// <summary>
        ///
        /// </summary>
        public const string OwinChallengeFlag = "X-Challenge";

        /// <summary>
        ///
        /// </summary>
        /// <param name="next"></param>
        public ResponseStatusCodeMiddleware(OwinMiddleware next) : base(next) { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task Invoke(IOwinContext context)
        {
            await Next.Invoke(context);

            if (context.Response.Headers.ContainsKey(OwinChallengeFlag))
            {
                var headerValue = context.Response.Headers.Get(OwinChallengeFlag);
                context.Response.StatusCode = Convert.ToInt16(headerValue);
                context.Response.Headers.Remove(OwinChallengeFlag);
            }
        }
    }
}