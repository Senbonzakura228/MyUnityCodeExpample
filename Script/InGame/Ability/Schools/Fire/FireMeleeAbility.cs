using UnityEngine;

public class FireMeleeAbility : Ability
{
    public override void Initialize()
    {
        base.Initialize();
        ApplyToHero();
    }

    private void ApplyToHero()
    {
        var meleeAttack = gameObject.AddComponent<FireMeleeAttack>();
        meleeAttack.SetStats(_value.Value);
        _heroModule.Hero.Attacking.ChangeMeleeAttack(meleeAttack);
    }
}