namespace LifeTimesStudy
{
    public class ScopedService
    {
        private readonly ScopedDisposableDep dep;

        public ScopedService(ScopedDisposableDep dep)
        {
            instanceId = Guid.NewGuid();
            this.dep = dep;
        }
        private readonly Guid instanceId;
        public Guid InstanceId
        {
            get
            {
                dep.Access();
                return instanceId;
            }
        }

    }
}
