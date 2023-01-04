using CodeBase.Infrastructure.Factory;

namespace CodeBase.Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices instance;
        public static AllServices Container => instance ??= new AllServices();

        public void RegisterSingle<TService>(TService imlementation) where TService : IService => 
            Implementatiom<TService>.ServiceInstance = imlementation;

        public TService Single<TService>() where TService : IService =>
            Implementatiom<TService>.ServiceInstance;

        private static class Implementatiom<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}