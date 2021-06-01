namespace Assets.Scripts.Cubes.Searching
{
    interface IEnemySearcher
    {
        void Initialize(Cube[] cubes);
        ITarget FindNearest(ITarget origin);
    }
}
