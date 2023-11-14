using UnityEngine;

public class LazerRangeAbility : Ability
{
    [SerializeField] private HeroCartridge cartridge;

    public override void Initialize()
    {
        base.Initialize();
        ApplyToHero();
    }

    public override void Upgrade()
    {
        base.Upgrade();
        gameObject.GetComponentInParent<LazerRangeAttack>().SetStats(_value.Value);
    }

    private void ApplyToHero()
    {
        var rangeAttack = gameObject.AddComponent<LazerRangeAttack>();
        rangeAttack.SetStats(_value.Value);
        _heroModule.Hero.Attacking.ChangeRangeAttack(rangeAttack, cartridge);
    }
}