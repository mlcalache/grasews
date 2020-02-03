namespace Grasews.Domain.Entities
{
    public abstract class BaseGraphNodePosition : BaseEntity<int>
    {
        #region Props

        public float X { get; set; }
        public float Y { get; set; }

        #endregion Props
    }
}