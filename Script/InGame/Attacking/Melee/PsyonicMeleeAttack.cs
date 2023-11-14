using UnityEngine;

public class PsyonicMeleeAttack : MeleeAttack
{
    private float _damageBoost;

    public void SetStats(float damageBoost, float radius)
    {
        _damageBoost = damageBoost;
        this.radius = radius;
    }

    public override void Attack(float damage, Transform position)
    {
        _damage = damage * (_damageBoost / 10);
        _attackPosition = position;
        _animator.SetBool("isSwordAttack", true);
    }
}