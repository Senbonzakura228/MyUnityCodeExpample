public class PsyonicMeleeAbility : Ability
{
    public override void Initialize()
    {
        base.Initialize();
        ApplyToHero();
    }

    private void ApplyToHero()
    {
        var meleeAttack = gameObject.AddComponent<PsyonicMeleeAttack>();
        meleeAttack.SetStats(_value.Value, _value.speedBoost);
        _heroModule.Hero.Attacking.ChangeMeleeAttack(meleeAttack);
    }
}