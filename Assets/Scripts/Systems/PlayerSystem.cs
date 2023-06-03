using Leopotam.EcsLite;
using UnityEngine;

public class PlayerSystem : IEcsInitSystem //ToDo: в одну систему с движением 
{
	private EcsPool<Player> _players;
	private EcsPool<Scores> _scores;
	private EcsPool<ScoresText> _scoresTexts;

	public void Init(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		int playerEntity = world.NewEntity();
		
		_players = world.GetPool<Player>();
		
		ref var player = ref _players.Add(playerEntity);
		var playerObject = GameObject.FindWithTag(Constants.PlayerTag); //ToDo
		player.Transform = playerObject.transform;
		
		_scores = world.GetPool<Scores>();
		_scores.Add(playerEntity);
	}
}
