using Leopotam.EcsLite;
using UnityEngine;

sealed class EcsStartup : MonoBehaviour
{
    private EcsWorld _world;
    private IEcsSystems _systems;

    private PoolService _poolService;
    private HudPresenter _hudPresenter;

    private void Start()
    {
        SetupPool();

        _world = new EcsWorld();

        _systems = new EcsSystems(_world);
        _systems
            .Add(new PlayerSystem())
            .Add(new PlayerMoveSystem())
            .Add(new RoadGeneratorSystem(_poolService))
            .Add(new CollectablesSystem(_poolService))
#if UNITY_EDITOR
            .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
            .Init();

        _hudPresenter = new HudPresenter(FindObjectOfType<Hud>(), _world);
    }

    private void Update()
    {
        _systems?.Run();
    }

    private void OnDestroy()
    {
        if (_systems != null)
        {
            _systems.Destroy();
            _systems = null;
        }

        if (_world != null)
        {
            _world.Destroy();
            _world = null;
        }
    }

    private void SetupPool()
    {
        _poolService = new PoolService();
        
        _poolService.AddPool<RoadPartView>(new []{
            "RoadParts/Road 1",
            "RoadParts/Road 2",
            "RoadParts/Road 3",
            "RoadParts/Road 4",
        }, 10, 10);
        
        _poolService.AddPool<CollectableView>(new []{
            "Collectable",
        }, 10, 10);
    }
}
