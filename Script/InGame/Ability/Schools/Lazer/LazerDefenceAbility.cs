using Hero.Script.InGame.Defence;

public class LazerDefenceAbility : Ability
{
    public override void Initialize()
    {
        base.Initialize();
        ApplyToHero();
    }

    private void ApplyToHero()
    {
        var defence = gameObject.AddComponent<LazerDefence>();
        defence.SetStats(_value.Value);
        _heroModule.Hero.SetHeroDefence(defence);
    }
}