namespace Grasews.Domain.Entities
{
    public class IssueAnswer : BaseEntity<int>
    {
        #region Props

        public string Answer { get; set; }
        public int IdIssue { get; set; }
        public int IdOwnerUser { get; set; }

        #endregion Props

        #region Navigations

        public virtual Issue Issue { get; set; }
        public virtual User OwnerUser { get; set; }

        #endregion Navigations
    }
}