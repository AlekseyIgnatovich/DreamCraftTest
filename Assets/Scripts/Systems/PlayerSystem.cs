using Leopotam.EcsLite;
using UnityEngine;

public class PlayerSystem : IEcsInitSystem //ToDo: в одну систему с движением 
{
	private EcsPool<Player> _players;

	public void Init(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		int playerEntity = world.NewEntity();
		
		_players = world.GetPool<Player>();
		
		ref var player = ref _players.Add(playerEntity);
		var car = GameObject.FindObjectOfType<CarView>(); //ToDo
		player.Transform = car.transform;
	}
}
