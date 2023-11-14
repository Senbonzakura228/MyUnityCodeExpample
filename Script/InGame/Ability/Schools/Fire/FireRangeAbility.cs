using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRangeAbility : Ability
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
        gameObject.GetComponentInParent<FireRangeAttack>().SetDamageBoost(_value.Value);
    }

    private void ApplyToHero()
    {
        var rangeAttack = gameObject.AddComponent<FireRangeAttack>();
        rangeAttack.SetDamageBoost(_value.Value);
        _heroModule.Hero.Attacking.ChangeRangeAttack(rangeAttack, cartridge);
    }
}