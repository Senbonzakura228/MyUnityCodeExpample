using UnityEngine;

public class ElectroRangeAttack : RangeAttack
{
    private float _electroDamageBoost;
    private float _speedBoost;

    public void SetStats(float electroDamage, float damageRadius)
    {
        _electroDamageBoost = electroDamage;
        _speedBoost = damageRadius;
    }

    public override void Attack(float damage, Transform position, HeroCartridge cartridge)
    {
        _damage = damage * (_electroDamageBoost / 10);
        _cartridge = cartridge;
        _cartridgeSpawnPosition = position;
        _animator.SetBool("isGunAttack", true);
    }

    public override void OnAttackAction()
    {
        ElectroCartridge cartridge =
            Instantiate(_cartridge, _cartridgeSpawnPosition.position, _cartridgeSpawnPosition.rotation) as
                ElectroCartridge;
        cartridge.SetParams(new ElectroMadaraCartridgeParams()
        {
            sortingLayerName = _spriteRenderer.sortingLayerName,
            damage = _damage,
            speedBoost = _speedBoost
        });
    }
}