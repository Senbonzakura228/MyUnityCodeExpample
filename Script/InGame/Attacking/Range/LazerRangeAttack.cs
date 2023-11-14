using UnityEngine;

public class LazerRangeAttack : RangeAttack
{
    private float _periodDamage;

    public void SetStats(float periodDamage)
    {
        _periodDamage = periodDamage;
    }

    public override void Attack(float damage, Transform position, HeroCartridge cartridge)
    {
        _cartridge = cartridge;
        _damage = damage;
        _cartridgeSpawnPosition = position;
        _animator.SetBool("isGunAttack", true);
    }

    public override void OnAttackAction()
    {
        LazerMadaraCartridge cartridge =
            Instantiate(_cartridge, _cartridgeSpawnPosition.position, _cartridgeSpawnPosition.rotation) as
                LazerMadaraCartridge;
        cartridge.SetParams(new LazerMadaraCartridgeInstantiateParams()
        {
            sortingLayerName = _spriteRenderer.sortingLayerName,
            damage = _damage,
            periodDamage = _periodDamage
        });
    }
}