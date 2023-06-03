using Leopotam.EcsLite;
using UnityEngine;

public class MoveSystem : IEcsInitSystem, IEcsRunSystem
{
    private const float Speed = 10f;//Todo
    private const float ManeuversShift = 3f;//Todo
    
    private EcsFilter _playersFilter;
    private EcsFilter _maneuverFilter;
        
    private EcsPool<Player> _players;
    private EcsPool<Maneuver> _maneuvers;
    
    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        _playersFilter = world.Filter<Player>().End();
        _maneuverFilter = world.Filter<Maneuver>().End();
        
        _players = world.GetPool<Player>();
        _maneuvers =  world.GetPool<Maneuver>();
    }
        
    public void Run(IEcsSystems systems)
    {
        foreach (var playerEntity in _playersFilter)
        {
            ref var player = ref _players.Get(playerEntity);
            
            var step = Speed * Time.deltaTime * Vector3.forward;
            player.Transform.position += step;

            foreach (var maneuverEntity in _maneuverFilter)
            {
                var maneuver = _maneuvers.Get(maneuverEntity);

                float shift = 0;
                switch (@maneuver.ManeuverDirection)
                {
                    case ManeuverDirection.Forward:
                        shift = 0;
                        break;
                    case ManeuverDirection.Left:
                        shift = -ManeuversShift;
                        break;
                    case ManeuverDirection.Right:
                        shift = ManeuversShift;
                        break;
                    default:
                        Debug.LogError("Unsupported Maneuver");
                        break;
                }
                
                player.Transform.position = new Vector3(shift, player.Transform.position.y, player.Transform.position.z);
                _maneuvers.Del(maneuverEntity);
            }
        }
    }
}
