using UnityEngine;

public class PsyonicRangeAbility : Ability
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
        gameObject.GetComponentInParent<PshyonicRangeAttack>().SetStats(_value.Value);
    }

    private void ApplyToHero()
    {
        var rangeAttack = gameObject.AddComponent<PshyonicRangeAttack>();
        rangeAttack.SetStats(_value.Value);
        _heroModule.Hero.Attacking.ChangeRangeAttack(rangeAttack, cartridge);
    }
}