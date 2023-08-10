namespace KingGame
{
    public interface IDependent<T>
    {
        public void SetDependency(T dependency);
    }
}