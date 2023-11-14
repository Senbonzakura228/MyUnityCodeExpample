using UnityEngine;

public class PshyonicRangeAttack : RangeAttack
{
    private float _afterDamage;

    public void SetStats(float afterDamage)
    {
        _afterDamage = afterDamage;
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
        PsyonicCartridge cartridge =
            Instantiate(_cartridge, _cartridgeSpawnPosition.position, _cartridgeSpawnPosition.rotation) as
                PsyonicCartridge;
        cartridge.SetParams(new PsyinicMadaraCartridgeInstantiateParams()
        {
            sortingLayerName = _spriteRenderer.sortingLayerName,
            damage = _damage,
            afterDamage = _afterDamage
        });
    }
}