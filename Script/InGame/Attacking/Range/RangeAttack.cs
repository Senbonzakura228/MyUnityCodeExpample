using System;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    protected Animator _animator;
    protected HeroCartridge _cartridge;
    protected Transform _cartridgeSpawnPosition;
    protected SpriteRenderer _spriteRenderer;
    protected float _damage;


    public void Initialize()
    {
        _animator = gameObject.GetComponentInParent<Animator>();
        _spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
    }

    public virtual void Attack(float damage, Transform position, HeroCartridge cartridge)
    {
        _cartridge = cartridge;
        _damage = damage;
        _cartridgeSpawnPosition = position;
        _animator.SetBool("isGunAttack", true);
    }

    public virtual void OnAttackAction()
    {
        var cartridge = Instantiate(_cartridge, _cartridgeSpawnPosition.position, _cartridgeSpawnPosition.rotation);
        cartridge.SetParams(new HeroCartridgeInstantiateParams()
            {sortingLayerName = _spriteRenderer.sortingLayerName, damage = _damage});
    }

    public void OnEndAttack()
    {
        _animator.SetBool("isGunAttack", false);
    }
}