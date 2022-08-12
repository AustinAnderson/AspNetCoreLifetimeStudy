using System.Diagnostics.CodeAnalysis;

namespace LifeTimesStudy
{
    public class ScopeWrapper<T> : IDisposable where T: notnull
    {
        private IServiceScope scope;
        private readonly IServiceScopeFactory scopeFactory;

        private T service;
        public T Service
        {
            //the service is a singleton within a given scope,
            get
            {
                if(service == null)
                {
                    scope = scopeFactory.CreateScope();
                    service = scope.ServiceProvider.GetRequiredService<T>();
                }
                return service;
            }
        }
        public ScopeWrapper(IServiceScopeFactory scopeFactory)
        {
            //creating the wrapper only resolves a factory
            this.scopeFactory = scopeFactory;
        }

        public void Dispose()
        {
            if (scope != null)
            {
                scope.Dispose();
            }
        }
    }
}
