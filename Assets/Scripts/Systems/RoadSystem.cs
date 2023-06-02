using Leopotam.EcsLite;
using UnityEngine;

public class RoadSystem : IEcsInitSystem, IEcsRunSystem
{
    private const float InstantiateRoadDistance = 70;//Todo
    private const float DestroyRoadDistance = 30;
    private const float RoadLength = 20;
    
    private EcsFilter _playersFilter;
    private EcsFilter _roadsFilter;
    
    private EcsPool<Player> _players;
    private EcsPool<RoadPart> _roadParts;
    
    private PoolService _poolService;
    
    public RoadSystem(PoolService poolService)
    {
        _poolService = poolService;
    }
    
    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        
        _playersFilter = world.Filter<Player>().End();
        _roadsFilter = world.Filter<RoadPart>().End();

        _roadParts = world.GetPool<RoadPart>();
        _players = world.GetPool<Player>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var playerEntity in _playersFilter)
        {
            var player = _players.Get(playerEntity);
            
            var maxDist = float.MinValue;
            var maxPosition = Vector3.back * RoadLength;
            foreach (var roadEntity in _roadsFilter)
            {
                var roadPart = _roadParts.Get(roadEntity);
                var dist = roadPart.Transform.position.z - player.Transform.position.z;

                if (dist < -DestroyRoadDistance)
                {
                    _poolService.GetPool<RoadPartView>().Release(roadPart.Transform.GetComponent<RoadPartView>()); //ToDo
                    _roadParts.Del(roadEntity);
                }

                if (dist > maxDist)
                {
                    maxDist = dist;
                    maxPosition = roadPart.Transform.position;
                }
            }

            if (maxDist < InstantiateRoadDistance)
            {        
                var world = systems.GetWorld();

                var roadEntity = world.NewEntity();
                ref var roadPart = ref _roadParts.Add(roadEntity);
                roadPart.Transform = _poolService.GetPool<RoadPartView>().Get().transform;
                roadPart.Transform.position = maxPosition + Vector3.forward * RoadLength;
            }
        }
    }
}
