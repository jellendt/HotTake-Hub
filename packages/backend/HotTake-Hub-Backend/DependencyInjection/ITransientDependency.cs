namespace HotTake_Hub_Backend.DependencyInjection
{
    /// <summary>
    ///     Marker interface for services that should be registered with Transient lifetime.
    ///     Services implementing this interface will be automatically registered by convention.
    /// </summary>
    public interface ITransientDependency
    {
    }
}
