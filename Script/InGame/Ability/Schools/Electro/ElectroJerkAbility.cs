using Hero.Script.InGame.Defence.Addition;
using Hero.Script.InGame.Jerk;
using UnityEngine;

public class ElectroJerkAbility : Ability
{
    [SerializeField] private PowerField powerFieldPrefab;

    public override void Initialize()
    {
        base.Initialize();
        ApplyToHero();
    }

    private void ApplyToHero()
    {
        var jerk = gameObject.AddComponent<ElectroJerk>();
        jerk.SetStats(_value.Value, _value.speedBoost, powerFieldPrefab);
        _heroModule.Hero.SetHeroJerk(jerk);
    }
}