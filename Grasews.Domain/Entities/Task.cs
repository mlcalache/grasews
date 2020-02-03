namespace Grasews.Domain.Entities
{
    using System.Collections.Generic;

    public class Task : BaseEntity<int>
    {
        #region Props

        public string Description { get; set; }
        public bool Done { get; set; }
        public int IdOwnerUser { get; set; }
        public int IdServiceDescription { get; set; }
        public string Title { get; set; }

        #endregion Props

        #region Navigations

        public virtual User OwnerUser { get; set; }
        public virtual ServiceDescription ServiceDescription { get; set; }
        public virtual ICollection<TaskComment> TaskComments { get; set; }

        #endregion Navigations

        #region ToString

        public override string ToString()
        {
            return Description;
        }

        #endregion ToString
    }
}