using UnityEngine;

public class LazerMeleeAttack : MeleeAttack
{
    private float _critDamageBoost;
    private float _critProbability = 25;
    public void SetStats(float critBoost)
    {
        _critDamageBoost = critBoost;
    }
    
    public override void Attack(float damage, Transform position)
    {
        _damage = damage;
        if (_critProbability >= Random.Range(0, 100))
        {
            _damage *= _critDamageBoost;
        }
        _attackPosition = position;
        _animator.SetBool("isSwordAttack", true);
    }
}