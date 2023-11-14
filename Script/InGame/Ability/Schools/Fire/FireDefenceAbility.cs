using Hero.Script.InGame.Defence;

public class FireDefenceAbility : Ability
{
    public override void Initialize()
    {
        base.Initialize();
        ApplyToHero();
    }

    private void ApplyToHero()
    {
        var defence = gameObject.AddComponent<FireDefence>();
        defence.SetStats(_value.Value);
        _heroModule.Hero.SetHeroDefence(defence);
    }
}