using UnityEngine;

public class FireMeleeAttack : MeleeAttack
{
    private float _damageBoost;
    public void SetStats(float damageBoost)
    {
        _damageBoost = damageBoost;
    }
    
    public override void Attack(float damage, Transform position)
    {
        _damage = damage * (_damageBoost / 10);
        _attackPosition = position;
        _animator.SetBool("isSwordAttack", true);
    }
}