using Leopotam.EcsLite;
using UnityEngine;

public class RoadSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _playersFilter;
    private EcsFilter _roadsFilter;
    
    private EcsPool<RoadPart> _roadParts;
    
    private PoolService _poolService;
    
    public RoadSystem(PoolService poolService)
    {
        _poolService = poolService;
    }
    
    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        var roadEntity = world.NewEntity();
        _roadParts = world.GetPool<RoadPart>();
        ref var roadPart = ref _roadParts.Add(roadEntity);
        roadPart.Transform = _poolService.GetPool<RoadPartView>().Get().transform;
        roadPart.Transform.position = Vector3.zero;
    }

    public void Run(IEcsSystems systems)
    {
        //throw new System.NotImplementedException();
    }
}
