namespace Grasews.Domain.Entities
{
    public class TaskComment : BaseEntity<int>
    {
        #region Props

        public string Comment { get; set; }
        public int IdOwnerUser { get; set; }
        public int IdTask { get; set; }

        #endregion Props

        #region Navigations

        public virtual User OwnerUser { get; set; }
        public virtual Task Task { get; set; }

        #endregion Navigations
    }
}