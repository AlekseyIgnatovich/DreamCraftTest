using Leopotam.EcsLite;
using UnityEngine;

public class PlayerSystem : IEcsInitSystem
{
	private EcsPool<Player> _players;
	private EcsPool<Position> _positions;

	public void Init(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		int playerEntity = world.NewEntity();
		
		_players = world.GetPool<Player>();
		_positions = world.GetPool<Position>();
		
		_players.Add(playerEntity);
		ref var position = ref _positions.Add(playerEntity);
		position.Value = Vector3.zero;
		
		var car = GameObject.FindObjectOfType<Car>();
		ref var transform = ref world.GetPool<TransformReference>().Add(playerEntity);
		transform.Transform = car.transform;
	}
}
