using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRangeAttack : RangeAttack
{
    private float _damageBoost;

    public void SetDamageBoost(float boost)
    {
        _damageBoost = boost;
    }

    public override void Attack(float damage, Transform position, HeroCartridge cartridge)
    {
        _cartridge = cartridge;
        _damage = damage * (_damageBoost / 10);
        _cartridgeSpawnPosition = position;
        _animator.SetBool("isGunAttack", true);
    }
}