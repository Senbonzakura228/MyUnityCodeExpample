using UnityEngine;

public class ElectroRangeAbility : Ability
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
        gameObject.GetComponentInParent<ElectroRangeAttack>().SetStats(_value.Value, _value.speedBoost);
    }

    private void ApplyToHero()
    {
        var rangeAttack = gameObject.AddComponent<ElectroRangeAttack>();
        rangeAttack.SetStats(_value.Value, _value.speedBoost);
        _heroModule.Hero.Attacking.ChangeRangeAttack(rangeAttack, cartridge);
    }
}