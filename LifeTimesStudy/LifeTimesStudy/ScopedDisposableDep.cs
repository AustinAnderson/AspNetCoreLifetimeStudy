namespace LifeTimesStudy
{
    public class ScopedDisposableDep : IDisposable
    {
        bool disposed = false;
        public void Access()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(ScopedDisposableDep));
            }
        }
        public void Dispose()
        {
            Console.WriteLine("disposable dep disposed");
            disposed = true;
        }
    }
}
