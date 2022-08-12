namespace LifeTimesStudy
{
    public static class InitializeExt
    {
        public static IServiceCollection AddSingletonWithInitializer<T>(this IServiceCollection services,Func<T,Task> onStart) where T : class
        {
            services.AddSingleton<T>();
            if(!services.Any(x=>x.ServiceType == typeof(StartupInitializeService)))
            {
                services.AddHostedService<StartupInitializeService>();
            }
            StartupInitializeService.InitializeInjections.Add(new InitializeInjection<T>(onStart));

            return services;
        }
    }
    public abstract class AbstractInitializeInjection
    {
        public abstract Type GetType();
        public abstract Task ExecuteStartup(object typeInstance);
    }
    public class InitializeInjection<T> : AbstractInitializeInjection where T: notnull
    {
        private readonly Func<T, Task> initializeAction;

        public InitializeInjection(Func<T,Task> initializeAction)
        {
            this.initializeAction = initializeAction;
        }
        public override async Task ExecuteStartup(object typeInstance)
        {
            await initializeAction((T)typeInstance);
        }
        public override Type GetType() => typeof(T);
    }
    public class StartupInitializeService:IHostedService
    {
        public static List<AbstractInitializeInjection> InitializeInjections = new List<AbstractInitializeInjection>();
        private readonly IServiceScopeFactory scopeFactory;

        public StartupInitializeService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using(var scope = scopeFactory.CreateScope())
            {
                foreach(var init in InitializeInjections)
                {
                    await init.ExecuteStartup(
                        scope.ServiceProvider.GetRequiredService(init.GetType())
                    );
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
