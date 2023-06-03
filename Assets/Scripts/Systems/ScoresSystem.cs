using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

public class ScoresSystem : IEcsInitSystem
{
	private EcsPool<Scores> _scores;
	private EcsPool<ScoresText> _scoresTexts;
	
    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        int scoresEntity = world.NewEntity();
		
        _scores = world.GetPool<Scores>();

        _scores.Add(scoresEntity);
    }
}
