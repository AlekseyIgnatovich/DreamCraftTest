using Leopotam.EcsLite;
using UnityEngine;

public class RoadGeneratorSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _playersFilter;
    private EcsFilter _roadsFilter;
    private EcsFilter _collectablesFilter;

    private EcsPool<Player> _players;
    private EcsPool<RoadPart> _roadParts;
    private EcsPool<Collectable> _collectables;

    private PoolService _poolService;

    public RoadGeneratorSystem(PoolService poolService)
    {
        _poolService = poolService;
    }

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _playersFilter = world.Filter<Player>().End();
        _roadsFilter = world.Filter<RoadPart>().End();
        _collectablesFilter = world.Filter<Collectable>().End();

        _roadParts = world.GetPool<RoadPart>();
        _players = world.GetPool<Player>();
        _collectables = world.GetPool<Collectable>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var playerEntity in _playersFilter)
        {
            var player = _players.Get(playerEntity);

            var maxDist = float.MinValue;
            var maxPosition = Vector3.back * Constants.RoadLength;
            foreach (var roadEntity in _roadsFilter)
            {
                var roadPart = _roadParts.Get(roadEntity);
                var dist = roadPart.View.transform.position.z - player.Transform.position.z;

                if (dist < -Constants.DestroyRoadDistance)
                {
                    _poolService.GetPool<RoadPartView>()
                        .Release(roadPart.View.transform.GetComponent<RoadPartView>());
                    _roadParts.Del(roadEntity);
                }

                if (dist > maxDist)
                {
                    maxDist = dist;
                    maxPosition = roadPart.View.transform.position;
                }
            }

            if (maxDist < Constants.InstantiateRoadDistance)
            {
                var world = systems.GetWorld();
                var roadPosition = maxPosition + Vector3.forward * Constants.RoadLength;

                var roadEntity = world.NewEntity();
                ref var roadPart = ref _roadParts.Add(roadEntity);
                roadPart.View = _poolService.GetPool<RoadPartView>().Get();
                roadPart.View.transform.position = roadPosition;

                var collectablePosZ = maxPosition.z + Constants.RoadLength / 2;
                int count = Random.Range(2, 6);
                for (int i = 0; i < count; i++)
                {
                    var shift = Random.value < 0.3 ? -Constants.ManeuversShift :
                        Random.value > 0.6 ? Constants.ManeuversShift : 0;
                    var collectablePosition = new Vector3(shift, 1, collectablePosZ + i * Constants.RoadLength / count);

                    var collectableEntity = world.NewEntity();
                    ref var collectable = ref _collectables.Add(collectableEntity);
                    collectable.View = _poolService.GetPool<CollectableView>().Get();
                    collectable.View.Init(world.PackEntityWithWorld(collectableEntity));
                    collectable.View.transform.position = collectablePosition;
                }
            }

            foreach (var collectableEntity in _collectablesFilter)
            {
                ref var collectable = ref _collectables.Get(collectableEntity);
                var dist = collectable.View.transform.position.z - player.Transform.position.z;
                if (dist < -Constants.DestroyRoadDistance)
                {
                    _poolService.GetPool<CollectableView>().Release(collectable.View);
                    _collectables.Del(collectableEntity);
                }
            }
        }
    }
}
