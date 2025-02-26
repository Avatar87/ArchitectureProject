namespace GameServer.Infrastructure.Scopes
{
    public interface IDependencyResolver
    {
        object Resolve(string dependency, object[] args);
    }
}
