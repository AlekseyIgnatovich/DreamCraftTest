using Leopotam.EcsLite;
using UnityEngine;

public class MoveSystem : IEcsInitSystem, IEcsRunSystem
{
    private const float Speed = 10f;

    private EcsFilter _playersFilter;
        
    private EcsPool<Position> _positions;
    private EcsPool<TransformReference> _transforms;
    
    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _playersFilter = world.Filter<Player>().End();
            
        _positions = world.GetPool<Position>();
        _transforms = world.GetPool<TransformReference>();
    }
        
    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _playersFilter)
        {
            ref var transform = ref _transforms.Get(entity);
            ref var position = ref _positions.Get(entity);
            
            var step = Speed * Time.deltaTime * Vector3.forward;
            position.Value += step;

            transform.Transform.position = position.Value;
        }
    }
}
