using Leopotam.EcsLite;
using UnityEngine;

public class PlayerSystem : IEcsInitSystem
{
	public void Init(IEcsSystems systems)
	{
		var world = systems.GetWorld();

		var players = world.GetPool<Player>();
		int playerEntity = world.NewEntity();
		ref var player = ref players.Add(playerEntity);
		var playerObject = GameObject.FindWithTag(Constants.PlayerTag); //ToDo
		player.Transform = playerObject.transform;

		var scores = world.GetPool<Scores>();
		scores.Add(playerEntity);
	}
}
