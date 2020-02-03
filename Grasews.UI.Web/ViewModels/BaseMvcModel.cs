namespace Grasews.Web.ViewModels
{
    public abstract class BaseMvcModel<TKey>
    {
        public TKey Id { get; set; }
    }
}