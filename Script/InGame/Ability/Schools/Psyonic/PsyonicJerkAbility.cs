using Hero.Script.InGame.Jerk;

public class PsyonicJerkAbility : Ability
{
    public override void Initialize()
    {
        base.Initialize();
        ApplyToHero();
    }

    private void ApplyToHero()
    {
        var jerk = gameObject.AddComponent<PsyonicJerk>();
        jerk.SetStats(_value.Value, _value.speedBoost);
        _heroModule.Hero.SetHeroJerk(jerk);
    }
}