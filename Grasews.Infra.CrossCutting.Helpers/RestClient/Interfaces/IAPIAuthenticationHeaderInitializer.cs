namespace Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces
{
    public interface IAPIAuthenticationHeaderInitializer
    {
        void Create(IAPIRestRequest APIRestRequest);
    }
}