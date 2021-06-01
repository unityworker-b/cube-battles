using Assets.Scripts.Cubes.Searching;
using Assets.Scripts.Cubes.Spawning;
using Zenject;

namespace Assets.Scripts.Ioc
{
    class BattleInstaller : MonoInstaller<BattleInstaller>
    {
        public CubeSpawnerParameters SpawnerParameters;

        public override void InstallBindings()
        {
            Container.Bind<SphericalCubeSpawner>().AsSingle().WithArguments(SpawnerParameters);
            Container.Bind<ICubeSpawner>().To<SphericalCubeSpawner>().FromResolve();

            Container.Bind<EnemySearcher>().AsSingle();
            Container.Bind<IEnemySearcher>().To<EnemySearcher>().FromResolve();

            Container.Bind<BattleRunner>().AsSingle().NonLazy();
        }
    }
}
