using Leopotam.EcsLite;
using UnityEngine;

public class MoveSystem : IEcsInitSystem, IEcsRunSystem
{
    private const float Speed = 10f;

    private EcsFilter _playersFilter;
        
    private EcsPool<Player> _players;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        _playersFilter = world.Filter<Player>().End();
        _players = world.GetPool<Player>();
    }
        
    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _playersFilter)
        {
            ref var player = ref _players.Get(entity);
            
            var step = Speed * Time.deltaTime * Vector3.forward;
            player.Transform.position += step;
        }
    }
}
