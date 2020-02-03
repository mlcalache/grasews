namespace Grasews.Domain.Entities
{
    public class ServiceDescription_User : BaseEntity<int>
    {
        #region Props

        public int IdServiceDescription { get; set; }
        public int IdSharedUser { get; set; }

        #endregion Props

        #region Navigations

        public virtual ServiceDescription ServiceDescription { get; set; }
        public virtual User SharedUser { get; set; }

        #endregion Navigations
    }
}