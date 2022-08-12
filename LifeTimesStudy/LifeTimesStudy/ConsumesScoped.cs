namespace LifeTimesStudy
{
    public class ConsumesScoped
    {
        private readonly ScopeWrapper<ScopedService> scope;

        public ConsumesScoped(ScopeWrapper<ScopedService> scope)
        {
            this.scope = scope;
            InstanceId = Guid.NewGuid();
        }
        Guid serviceGuid= Guid.Empty;
        public async Task<Guid> GetScopedsInstanceId()
        {
            if(serviceGuid == Guid.Empty)
            {
                await Task.Delay(7000);
                var service = scope.Service;
                serviceGuid = service.InstanceId;
                //if dispose is called before service.instance, the scope will be disposed,
                //meaning the scopedDisposableDep down the chain will be disposed and we'll get ObjectDisposedException
                scope.Dispose();
            }
            return serviceGuid;
        } 

        public Guid InstanceId { get; }
    }
}
