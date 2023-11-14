public class LazerMeleeAbility : Ability
{
    public override void Initialize()
    {
        base.Initialize();
        ApplyToHero();
    }

    private void ApplyToHero()
    {
        var meleeAttack = gameObject.AddComponent<LazerMeleeAttack>();
        meleeAttack.SetStats(_value.Value);
        _heroModule.Hero.Attacking.ChangeMeleeAttack(meleeAttack);
    }
}