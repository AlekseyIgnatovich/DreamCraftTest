using Leopotam.EcsLite;

public class CollectablesSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsFilter _scoresfilter;

    private EcsPool<Collectable> _collectables;
    private EcsPool<Hit> _hits;
    private EcsPool<Scores> _scores;
    private EcsPool<ScoresText> _scoresText;

    private PoolService _poolService;

    public CollectablesSystem(PoolService poolService)
    {
        _poolService = poolService;
    }

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<Collectable>().Inc<Hit>().End();
        _scoresfilter = world.Filter<Scores>().Inc<ScoresText>().End();

        _collectables = world.GetPool<Collectable>();
        _hits = world.GetPool<Hit>();

        _scores = world.GetPool<Scores>();
        _scoresText = world.GetPool<ScoresText>();
    }

    public void Run(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        foreach (var entity in _filter)
        {
            var collectable = _collectables.Get(entity);
            _poolService.GetPool<CollectableView>().Release(collectable.View);
            _collectables.Del(entity);
            _hits.Del(entity);

            foreach (var scoresEntity in _scoresfilter)
            {
                ref var score = ref _scores.Get(scoresEntity);
                score.Value++;

                var scoreText = _scoresText.Get(scoresEntity);
                scoreText.Text.text = score.Value.ToString();
            }
        }
    }
}
