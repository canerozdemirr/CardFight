namespace _Game.Scripts.Interfaces
{
    public interface IPoolable
    {
        void OnCalledFromPool();
        void OnReturnToPool();
    }
}
