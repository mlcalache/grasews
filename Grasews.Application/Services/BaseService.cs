using Grasews.Domain.Interfaces.Services;
using System;

namespace Grasews.Application.Services
{
    public abstract class BaseService : IBaseService
    {
        #region Ctors

        protected BaseService()
        {
        }

        #endregion Ctors

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}