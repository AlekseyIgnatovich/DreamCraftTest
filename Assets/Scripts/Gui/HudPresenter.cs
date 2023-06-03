using Leopotam.EcsLite;

public class HudPresenter
{
    private Hud _hud;
    private EcsWorld _world;

    public HudPresenter(Hud hud, EcsWorld world)
    {
        _hud = hud;
        _world = world;

        _hud.OnPressLeftButton += () => { ChangeManeuverDirection(ManeuverDirection.Left); };
        _hud.OnPressRightButton += () => { ChangeManeuverDirection(ManeuverDirection.Right); };

        _hud.OnFinishPress += () => { ChangeManeuverDirection(ManeuverDirection.Forward); };

        var pool = _world.GetPool<ScoresText>();
        var scoresTexts = _world.Filter<Scores>().End();
        foreach (var entity in scoresTexts)
        {
            ref var scoresText = ref pool.Add(entity);
            scoresText.Text = _hud.ScoresText;
        }
    }

    private void ChangeManeuverDirection(ManeuverDirection maneuver)
    {
        var players = _world.Filter<Player>().End();

        foreach (var player in players)
        {
            var pool = _world.GetPool<Maneuver>();
            ref var direction = ref pool.Add(player);
            direction.ManeuverDirection = maneuver;
        }
    }
}
