using Assets.Scripts.Cubes;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Ioc
{
    class CubeInstaller : MonoInstaller<CubeInstaller>
    {
        public GameObject CubePrefab;
        public CubeParameters CubeParameters;

        public override void InstallBindings()
        {
            Container.BindInstance(CubeParameters).AsCached();
            Container.BindFactory<Cube, Cube.Factory>().FromComponentInNewPrefab(CubePrefab).AsSingle();
            Container.Bind<IFactory<Cube>>().To<Cube.Factory>().FromResolve();
        }
    }
}
